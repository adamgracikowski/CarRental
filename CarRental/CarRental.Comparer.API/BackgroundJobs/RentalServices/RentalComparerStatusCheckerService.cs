using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Comparer.Infrastructure.CarComparisons;
using Hangfire;

namespace CarRental.Comparer.API.BackgroundJobs.RentalServices;

public class RentalComparerStatusCheckerService : IRentalComparerStatusCheckerService
{
    private readonly IRepositoryBase<RentalTransaction> rentalTransactionsRepository;
    private readonly ILogger<RentalComparerStatusCheckerService> logger;
    private readonly ICarComparisonService carComparisonService;
    private readonly IDateTimeProvider dateTimeProvider;

    public RentalComparerStatusCheckerService(
        IRepositoryBase<RentalTransaction> rentalTransactionsRepository,
        ILogger<RentalComparerStatusCheckerService> logger,
        ICarComparisonService carComparisonService,
        IDateTimeProvider dateTimeProvider)
    {
        this.rentalTransactionsRepository = rentalTransactionsRepository;
        this.logger = logger;
        this.carComparisonService = carComparisonService;
        this.dateTimeProvider = dateTimeProvider;
    }

    public async Task CheckAndUpdateRentalStatusAsync(
        string providerName, 
        int innerRentalId, 
        int outerRentalId, 
        string jobId, 
        DateTime jobExpirationTime, 
        CancellationToken cancellationToken)
    {
        if (dateTimeProvider.Now >= jobExpirationTime)
        {
            logger.LogInformation($"Job with Id = {jobId} has expired.");
            RecurringJob.RemoveIfExists(jobId);
            return;
        }
        
        var rentalStatusDto = await carComparisonService.GetRentalStatusByIdAsync(providerName, outerRentalId, cancellationToken);

        if(rentalStatusDto is null)
        {
            logger.LogInformation($"Rental with Id = {outerRentalId} does not exist in providerDB.");
            RecurringJob.RemoveIfExists(jobId);
            return;
        }

        if(rentalStatusDto.status == RentalStatus.Active.ToString() || rentalStatusDto.status == RentalStatus.Rejected.ToString())
        {
            var rentalTransaction = await rentalTransactionsRepository.GetByIdAsync(innerRentalId, cancellationToken);

            if(rentalTransaction is null)
            {
                logger.LogInformation($"RentalTransaction with id = {innerRentalId} does not exist in comparerDB.");
                RecurringJob.RemoveIfExists(jobId);
                return;
            }

            rentalTransaction.Status = rentalStatusDto.status == RentalStatus.Active.ToString() ? RentalStatus.Active : RentalStatus.Rejected;

            await rentalTransactionsRepository.UpdateAsync(rentalTransaction, cancellationToken);
            await rentalTransactionsRepository.SaveChangesAsync(cancellationToken);

            RecurringJob.RemoveIfExists(jobId);
            return;
        }
    }
}
