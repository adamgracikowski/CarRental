using Ardalis.Specification;
using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.Persistence.Specifications.Rentals;
using Microsoft.Extensions.Logging;

namespace CarRental.Provider.Infrastructure.BackgroundJobs.RentalServices;

public sealed class RentalStatusCheckerService : IRentalStatusCheckerService
{
    private readonly IRepositoryBase<Rental> rentalsRepository;
    private readonly ILogger<RentalStatusCheckerService> logger;

    public RentalStatusCheckerService(
        IRepositoryBase<Rental> rentalRepository,
        ILogger<RentalStatusCheckerService> logger)
    {
        this.rentalsRepository = rentalRepository;
        this.logger = logger;
    }

    public async Task CheckAndUpdateRentalStatusAsync(int id, CancellationToken cancellationToken)
    {
        var specification = new RentalByIdSpecification(id);
        var rental = await this.rentalsRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (rental == null)
        {
            this.logger.LogInformation($"Rental with Id = {id} does not exist.");
            return;
        }

        if (rental.Status != RentalStatus.Unconfirmed)
        {
            this.logger.LogInformation($"Rental status different from {nameof(RentalStatus.Unconfirmed)}.");
            return;
        }

        rental.Status = RentalStatus.Rejected;

        await this.rentalsRepository.UpdateAsync(rental, cancellationToken);
        await this.rentalsRepository.SaveChangesAsync(cancellationToken);

        this.logger.LogInformation($"Rental with Id = {id} has been {nameof(RentalStatus.Rejected)}.");
    }
}
