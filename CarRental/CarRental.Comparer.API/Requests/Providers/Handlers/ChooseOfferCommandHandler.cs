using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.Requests.Providers.Commands;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using FluentValidation;
using MediatR;
using Ardalis.Result.FluentValidation;
using CarRental.Comparer.Persistence.Specifications.Users;
using Hangfire;
using CarRental.Comparer.API.BackgroundJobs.RentalServices;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;

namespace CarRental.Comparer.API.Requests.Providers.Handlers;

public class ChooseOfferCommandHandler : IRequestHandler<ChooseOfferCommand, Result<RentalTransactionIdWithDateTimesDto>>
{
    private readonly IRepositoryBase<Provider> providersRepository;
    private readonly ILogger<ChooseOfferCommandHandler> logger;
    private readonly ICarComparisonService carComparisonService;
    private readonly IValidator<ChooseOfferDto> validator;
    private readonly IRepositoryBase<User> usersRepository;
    private readonly IRepositoryBase<CarDetails> carDetailsRepository;
    private readonly IRepositoryBase<RentalTransaction> rentalTransactionsRepository;
    private readonly IDateTimeProvider dateTimeProvider; 
    private readonly IRentalComparerStatusCheckerService rentalComparerStatusCheckerService;

    public ChooseOfferCommandHandler(IRepositoryBase<Provider> providersRepository,
        ILogger<ChooseOfferCommandHandler> logger,
        ICarComparisonService carComparisonService,
        IValidator<ChooseOfferDto> validator,
        IRepositoryBase<User> usersRepository,
        IRepositoryBase<CarDetails> carDetailsRepository,
        IRepositoryBase<RentalTransaction> rentalTransactionsRepository,
        IDateTimeProvider dateTimeProvider,
        IRentalComparerStatusCheckerService rentalComparerStatusCheckerService)
    {
        this.providersRepository = providersRepository;
        this.logger = logger;
        this.carComparisonService = carComparisonService;
        this.validator = validator;
        this.usersRepository = usersRepository;
        this.carDetailsRepository = carDetailsRepository;
        this.rentalTransactionsRepository = rentalTransactionsRepository;
        this.dateTimeProvider = dateTimeProvider;
        this.rentalComparerStatusCheckerService = rentalComparerStatusCheckerService;
    }

    public async Task<Result<RentalTransactionIdWithDateTimesDto>> Handle(ChooseOfferCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request.chooseOfferDto, cancellationToken);

        if (!validation.IsValid)
        {
            return Result<RentalTransactionIdWithDateTimesDto>.Invalid(validation.AsErrors());
        }

        var provider = await providersRepository.GetByIdAsync(request.id, cancellationToken);

        if (provider?.Name is null)
        {
            logger.LogWarning($"Provider with ID {request.id} not found.");
            return Result<RentalTransactionIdWithDateTimesDto>.NotFound();
        }

        var userSpecification = new UserByEmailSpecification(request.chooseOfferDto.EmailAddress);

        var user = await usersRepository.FirstOrDefaultAsync(userSpecification, cancellationToken);

        if (user is null)
        {
            logger.LogWarning($"User with email {request.chooseOfferDto.EmailAddress} not found.");
            return Result<RentalTransactionIdWithDateTimesDto>.NotFound();
        }

        var providerChooseOfferDto = new ProviderChooseOfferDto(
            user.Email,
            user.Name,
            user.Lastname
        );

        var outerRentalIdWithDateTimesDto = await carComparisonService.ChooseOfferAsync(
            provider.Name, 
            request.offerId, 
            providerChooseOfferDto, 
            cancellationToken);

        if (outerRentalIdWithDateTimesDto is null)
        {
            return Result<RentalTransactionIdWithDateTimesDto>.Error();
        }

        var carDetails = new CarDetails
        {
            OuterId = request.chooseOfferDto.CarOuterId,
            Make = request.chooseOfferDto.Make,
            Model = request.chooseOfferDto.Model,
            Segment = request.chooseOfferDto.Segment,
            FuelType = request.chooseOfferDto.FuelType,
            TransmissionType = request.chooseOfferDto.TransmissionType,
            YearOfProduction = request.chooseOfferDto.YearOfProduction,
            NumberOfDoors = request.chooseOfferDto.NumberOfDoors,
            NumberOfSeats = request.chooseOfferDto.NumberOfSeats
        };

        await carDetailsRepository.AddAsync(carDetails, cancellationToken);
        await carDetailsRepository.SaveChangesAsync(cancellationToken);

        var rentalTransaction = new RentalTransaction
        {
            User = user,
            Provider = provider,
            CarDetails = carDetails,
            RentalOuterId = outerRentalIdWithDateTimesDto.id,
            RentalPricePerDay = request.chooseOfferDto.RentalPricePerDay,
            InsurancePricePerDay = request.chooseOfferDto.InsurancePricePerDay,
            RentedAt = outerRentalIdWithDateTimesDto.GeneratedAt,
        };

        await rentalTransactionsRepository.AddAsync(rentalTransaction, cancellationToken);
        await rentalTransactionsRepository.SaveChangesAsync(cancellationToken);

        var comparerRentalTransactionIdDto = new RentalTransactionIdWithDateTimesDto(
            rentalTransaction.Id, 
            rentalTransaction.RentedAt, 
            outerRentalIdWithDateTimesDto.ExpiresAt);

        var recurringJobId = $"RentalStatusChecker-{rentalTransaction.Id}";

        RecurringJob.AddOrUpdate(
            recurringJobId,
            () => rentalComparerStatusCheckerService.CheckAndUpdateRentalStatusAsync(
                provider.Name, 
                comparerRentalTransactionIdDto.Id,
                outerRentalIdWithDateTimesDto.id,
                recurringJobId,
                outerRentalIdWithDateTimesDto.ExpiresAt.AddMinutes(5),
                cancellationToken), 
            Cron.Minutely);

        return Result<RentalTransactionIdWithDateTimesDto>.Success(comparerRentalTransactionIdDto);
    }
}