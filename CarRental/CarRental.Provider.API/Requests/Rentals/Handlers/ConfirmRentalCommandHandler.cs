using Ardalis.Result;
using Ardalis.Specification;
using Azure.Core;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.DTOs.RentalReturns;
using CarRental.Provider.API.Requests.Rentals.Commands;
using CarRental.Provider.Persistence.Specifications.Rentals;
using CarRental.Common.Core.Enums;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Handlers;

public sealed class ConfirmRentalCommandHandler : IRequestHandler<ConfirmRentalCommand, Result>
{
    private IRepositoryBase<Rental> rentalsRepository;

    public ConfirmRentalCommandHandler(IRepositoryBase<Rental> rentalsRepository)
    {
        this.rentalsRepository = rentalsRepository;
    }
    public async Task<Result> Handle(ConfirmRentalCommand command, CancellationToken cancellationToken)
    {
        var specification = new RentalByIdWithOfferCarSpecification(command.Id);
        var rental = await this.rentalsRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (rental == null)
        {
            return Result.NotFound();
        }

        if (rental.Status == RentalStatus.Active)
        {
            return Result.Success();
        }

        if (rental.Status != RentalStatus.Unconfirmed)
        {
            return Result.Invalid(new ValidationError(nameof(Rental.Status), "Rental is not in unconfirmed state."));
        }

        if (rental.Offer.Key != command.Key)
        {
            return Result.Invalid(new ValidationError(command.Key, "Key is not valid."));
        }

        if (rental.Offer.Car.Status == CarStatus.Rented)
        {
            return Result.Invalid(new ValidationError("CarStatus", "Car is not available."));
        }

        rental.Status = RentalStatus.Active;
        rental.Offer.Car.Status = CarStatus.Rented;

        await this.rentalsRepository.UpdateAsync(rental, cancellationToken);
        await this.rentalsRepository.SaveChangesAsync(cancellationToken);

        //TODO: send email

        return Result.Success();

    }
}

