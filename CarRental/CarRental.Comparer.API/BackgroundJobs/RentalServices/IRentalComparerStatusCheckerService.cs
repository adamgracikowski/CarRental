namespace CarRental.Comparer.API.BackgroundJobs.RentalServices;

public interface IRentalComparerStatusCheckerService
{
    Task CheckAndUpdateRentalStatusAsync(
        string providerName, 
        int rentalTransactionId, 
        string outerRentalId, 
        string jobId, 
        DateTime jobExpirationTime, 
        CancellationToken cancellationToken);
}