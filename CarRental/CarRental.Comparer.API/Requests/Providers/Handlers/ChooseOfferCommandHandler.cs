using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.BackgroundJobs.RentalServices;
using CarRental.Comparer.API.Requests.Providers.Commands;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;
using CarRental.Comparer.Persistence.Specifications.Users;
using Hangfire;
using MediatR;

namespace CarRental.Comparer.API.Requests.Providers.Handlers;

public sealed class ChooseOfferCommandHandler : IRequestHandler<ChooseOfferCommand, Result<RentalTransactionIdWithDateTimesDto>>
{
    private readonly IRepositoryBase<Provider> providersRepository;
    private readonly ILogger<ChooseOfferCommandHandler> logger;
    private readonly ICarComparisonService carComparisonService;
    private readonly IRepositoryBase<User> usersRepository;
    private readonly IRepositoryBase<CarDetails> carDetailsRepository;
    private readonly IRepositoryBase<RentalTransaction> rentalTransactionsRepository;
    private readonly IRentalComparerStatusCheckerService rentalComparerStatusCheckerService;

    public ChooseOfferCommandHandler(IRepositoryBase<Provider> providersRepository,
        ILogger<ChooseOfferCommandHandler> logger,
        ICarComparisonService carComparisonService,
        IRepositoryBase<User> usersRepository,
        IRepositoryBase<CarDetails> carDetailsRepository,
        IRepositoryBase<RentalTransaction> rentalTransactionsRepository,
        IRentalComparerStatusCheckerService rentalComparerStatusCheckerService)
    {
        this.providersRepository = providersRepository;
        this.logger = logger;
        this.carComparisonService = carComparisonService;
        this.usersRepository = usersRepository;
        this.carDetailsRepository = carDetailsRepository;
        this.rentalTransactionsRepository = rentalTransactionsRepository;
        this.rentalComparerStatusCheckerService = rentalComparerStatusCheckerService;
    }

    public async Task<Result<RentalTransactionIdWithDateTimesDto>> Handle(ChooseOfferCommand request, CancellationToken cancellationToken)
    {
        var provider = await this.providersRepository.GetByIdAsync(request.Id, cancellationToken);

        if (provider?.Name is null)
        {
            this.logger.LogWarning($"Provider with ID {request.Id} not found.");
            return Result<RentalTransactionIdWithDateTimesDto>.NotFound();
        }

        var userSpecification = new UserByEmailSpecification(request.ChooseOfferDto.EmailAddress);

        var user = await this.usersRepository.FirstOrDefaultAsync(userSpecification, cancellationToken);

        if (user is null)
        {
			this.logger.LogWarning($"User with email {request.ChooseOfferDto.EmailAddress} not found.");
            return Result<RentalTransactionIdWithDateTimesDto>.NotFound();
        }

        var providerChooseOfferDto = new ProviderChooseOfferDto(
            user.Email,
            user.Name,
            user.Lastname
        );

        var outerRentalIdWithDateTimesDto = await this.carComparisonService.ChooseOfferAsync(
            provider.Name, 
            request.OfferId, 
            providerChooseOfferDto, 
            cancellationToken);

        if (outerRentalIdWithDateTimesDto is null)
        {
            return Result<RentalTransactionIdWithDateTimesDto>.Error();
        }

        var carDetails = new CarDetails
        {
            OuterId = request.ChooseOfferDto.CarOuterId,
            Make = request.ChooseOfferDto.Make,
            Model = request.ChooseOfferDto.Model,
            Segment = request.ChooseOfferDto.Segment,
            FuelType = request.ChooseOfferDto.FuelType,
            TransmissionType = request.ChooseOfferDto.TransmissionType,
            YearOfProduction = request.ChooseOfferDto.YearOfProduction,
            NumberOfDoors = request.ChooseOfferDto.NumberOfDoors,
            NumberOfSeats = request.ChooseOfferDto.NumberOfSeats
        };

        await this.carDetailsRepository.AddAsync(carDetails, cancellationToken);
        await this.carDetailsRepository.SaveChangesAsync(cancellationToken);

        var rentalTransaction = new RentalTransaction
        {
            User = user,
            Provider = provider,
            CarDetails = carDetails,
            RentalOuterId = outerRentalIdWithDateTimesDto.Id,
            RentalPricePerDay = request.ChooseOfferDto.RentalPricePerDay,
            InsurancePricePerDay = request.ChooseOfferDto.InsurancePricePerDay,
            RentedAt = outerRentalIdWithDateTimesDto.GeneratedAt,
        };

        await this.rentalTransactionsRepository.AddAsync(rentalTransaction, cancellationToken);
        await this.rentalTransactionsRepository.SaveChangesAsync(cancellationToken);

        var comparerRentalTransactionIdDto = new RentalTransactionIdWithDateTimesDto(
            rentalTransaction.Id, 
            rentalTransaction.RentedAt, 
            outerRentalIdWithDateTimesDto.ExpiresAt
        );

        var recurringJobId = $"RentalStatusChecker-{rentalTransaction.Id}";

        RecurringJob.AddOrUpdate(
            recurringJobId,
            () => this.rentalComparerStatusCheckerService.CheckAndUpdateRentalStatusAsync(
                provider.Name, 
                comparerRentalTransactionIdDto.Id,
                outerRentalIdWithDateTimesDto.Id,
                recurringJobId,
                outerRentalIdWithDateTimesDto.ExpiresAt.AddMinutes(5),
                cancellationToken
            ), Cron.Minutely
        );

        return Result<RentalTransactionIdWithDateTimesDto>.Success(comparerRentalTransactionIdDto);
    }
}