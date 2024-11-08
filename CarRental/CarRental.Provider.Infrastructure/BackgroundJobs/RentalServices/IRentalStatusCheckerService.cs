namespace CarRental.Provider.Infrastructure.BackgroundJobs.RentalServices;

public interface IRentalStatusCheckerService
{
    Task CheckAndUpdateRentalStatusAsync(int id, CancellationToken cancellationToken);
}