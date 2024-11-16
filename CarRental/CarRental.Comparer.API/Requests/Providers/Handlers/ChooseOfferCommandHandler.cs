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

namespace CarRental.Comparer.API.Requests.Providers.Handlers;

public class ChooseOfferCommandHandler : IRequestHandler<ChooseOfferCommand, Result<RentalIdDto>>
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

    public async Task<Result<RentalIdDto>> Handle(ChooseOfferCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request.chooseOfferDto, cancellationToken);

        if (!validation.IsValid)
        {
            return Result<RentalIdDto>.Invalid(validation.AsErrors());
        }

        var provider = await providersRepository.GetByIdAsync(request.id, cancellationToken);

        if (provider?.Name is null)
        {
            logger.LogWarning($"Provider with ID {request.id} not found.");
            return Result<RentalIdDto>.NotFound();
        }

        var userSpecification = new UserByEmailSpecification(request.chooseOfferDto.EmailAddress);

        var user = await usersRepository.FirstOrDefaultAsync(userSpecification, cancellationToken);

        if (user is null)
        {
            logger.LogWarning($"User with email {request.chooseOfferDto.EmailAddress} not found.");
            return Result<RentalIdDto>.NotFound();
        }

        var providerChooseOfferDto = new ProviderChooseOfferDto(
            user.Email,
            user.Name,
            user.Lastname
        );

        var outerRentalIdDto = await carComparisonService.ChooseOfferAsync(provider.Name, request.offerId, providerChooseOfferDto, cancellationToken);

        if (outerRentalIdDto is null)
        {
            return Result<RentalIdDto>.Error();
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
            UserId = user.Id,
            ProviderId = provider.Id,
            RentalOuterId = outerRentalIdDto.id.ToString(),
            RentalPricePerDay = request.chooseOfferDto.RentalPricePerDay,
            InsurancePricePerDay = request.chooseOfferDto.InsurancePricePerDay,
            RentedAt = request.chooseOfferDto.RentedAt,
            CarDetailsId = carDetails.Id,
        };

        await rentalTransactionsRepository.AddAsync(rentalTransaction, cancellationToken);
        await rentalTransactionsRepository.SaveChangesAsync(cancellationToken);

        var comparerRentalIdDto = new RentalIdDto(rentalTransaction.Id);

        var recurringJobId = $"RentalStatusChecker-{rentalTransaction.Id}";

        RecurringJob.AddOrUpdate(
            recurringJobId,
            () => rentalComparerStatusCheckerService.CheckAndUpdateRentalStatusAsync(
                provider.Name, 
                comparerRentalIdDto.id,
                outerRentalIdDto.id,
                recurringJobId,
                request.chooseOfferDto.ExpiresAt.AddMinutes(5),
                cancellationToken), 
            Cron.Minutely);

        return Result<RentalIdDto>.Success(comparerRentalIdDto);
    }
}