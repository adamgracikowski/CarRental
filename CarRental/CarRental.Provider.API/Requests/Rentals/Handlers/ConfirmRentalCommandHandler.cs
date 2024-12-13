using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Provider.API.Requests.Rentals.Commands;
using CarRental.Provider.Infrastructure.EmailServices;
using CarRental.Provider.Persistence.Specifications.Rentals;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Handlers;

public sealed class ConfirmRentalCommandHandler : IRequestHandler<ConfirmRentalCommand, Result>
{
    private IRepositoryBase<Rental> rentalsRepository;
    private readonly IEmailService emailService;
	private readonly IDateTimeProvider dateTimeProvider;
	private readonly IEmailInputMaker emailInputMaker;

    public ConfirmRentalCommandHandler(
        IRepositoryBase<Rental> rentalsRepository,
        IEmailInputMaker emailInputMaker,
        IEmailService emailService,
        IDateTimeProvider dateTimeProvider)
    {
        this.rentalsRepository = rentalsRepository;
        this.emailInputMaker = emailInputMaker;
        this.emailService = emailService;
		this.dateTimeProvider = dateTimeProvider;
	}
    public async Task<Result> Handle(ConfirmRentalCommand command, CancellationToken cancellationToken)
    {
        var specification = new RentalByIdWithOfferCarModelMakeClientSpecification(command.Id);
        var rental = await this.rentalsRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (rental == null)
        {
            return Result.NotFound();
        }

        if (rental.Offer.ExpiresAt <= this.dateTimeProvider.UtcNow)
        {
			return Result.Invalid(new ValidationError(nameof(Offer.ExpiresAt), "The offer associated with rental has expired."));
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

        var emailInput = this.emailInputMaker.GenerateRentalConfirmedInput(
            rental.Customer.EmailAddress,
            $"{rental.Customer.FirstName} {rental.Customer.LastName}",
            rental.Offer.Car.Model.Make.Name,
            rental.Offer.Car.Model.Name
        );

        await this.emailService.SendEmailAsync(emailInput);

        return Result.Success();
    }
}