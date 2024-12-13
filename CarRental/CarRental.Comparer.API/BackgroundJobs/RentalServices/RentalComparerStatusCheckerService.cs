using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Infrastructure.CarProviders.RentalStatusConversions;
using Hangfire;

namespace CarRental.Comparer.API.BackgroundJobs.RentalServices;

public class RentalComparerStatusCheckerService : IRentalComparerStatusCheckerService
{
	private readonly IRepositoryBase<RentalTransaction> rentalTransactionsRepository;
	private readonly ILogger<RentalComparerStatusCheckerService> logger;
	private readonly ICarComparisonService carComparisonService;
	private readonly IDateTimeProvider dateTimeProvider;
	private readonly IRentalStatusConverter rentalStatusConverter;

	public RentalComparerStatusCheckerService(
		IRepositoryBase<RentalTransaction> rentalTransactionsRepository,
		ILogger<RentalComparerStatusCheckerService> logger,
		ICarComparisonService carComparisonService,
		IDateTimeProvider dateTimeProvider,
		IRentalStatusConverter rentalStatusConverter)
	{
		this.rentalTransactionsRepository = rentalTransactionsRepository;
		this.logger = logger;
		this.carComparisonService = carComparisonService;
		this.dateTimeProvider = dateTimeProvider;
		this.rentalStatusConverter = rentalStatusConverter;
	}

	public async Task CheckAndUpdateRentalStatusAsync(
		string providerName,
		int rentalTransactionId,
		string outerRentalId,
		string jobId,
		DateTime jobExpirationTime,
		CancellationToken cancellationToken = default)
	{
		if (this.dateTimeProvider.UtcNow >= jobExpirationTime)
		{
			this.logger.LogInformation($"Job with Id = {jobId} has expired.");
			RecurringJob.RemoveIfExists(jobId);
			return;
		}

		var rentalStatusDto = await this.carComparisonService.GetRentalStatusByIdAsync(providerName, outerRentalId, cancellationToken);

		if (rentalStatusDto is null)
		{
			this.logger.LogInformation($"Rental with Id = {outerRentalId} does not exist in providerDB.");
			RecurringJob.RemoveIfExists(jobId);
			return;
		}

		if (this.rentalStatusConverter.AreEquivalent(RentalStatus.Active, rentalStatusDto.Status, providerName) ||
		   this.rentalStatusConverter.AreEquivalent(RentalStatus.Rejected, rentalStatusDto.Status, providerName))
		{

			var rentalTransaction = await rentalTransactionsRepository.GetByIdAsync(rentalTransactionId, cancellationToken);

			if (rentalTransaction is null)
			{
				logger.LogInformation($"RentalTransaction with id = {rentalTransactionId} does not exist in comparerDB.");
				RecurringJob.RemoveIfExists(jobId);
				return;
			}

			if (!this.rentalStatusConverter.TryConvertFromProviderRentalStatus(rentalStatusDto.Status, providerName, out var updatedStatus))
			{
				logger.LogInformation($"Rental status conversion for {rentalStatusDto.Status} has failed.");
				RecurringJob.RemoveIfExists(jobId);
				return;
			}

			rentalTransaction.Status = updatedStatus;

			await rentalTransactionsRepository.UpdateAsync(rentalTransaction, cancellationToken);
			await rentalTransactionsRepository.SaveChangesAsync(cancellationToken);

			RecurringJob.RemoveIfExists(jobId);
			return;
		}

		this.logger.LogInformation($"Rental status different from Active or Rejected... Trying again.");
	}
}