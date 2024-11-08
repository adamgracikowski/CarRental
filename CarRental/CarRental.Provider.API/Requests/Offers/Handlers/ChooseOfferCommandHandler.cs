using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Provider.API.Requests.Offers.Commands;
using CarRental.Provider.API.Requests.Rentals.DTOs;
using CarRental.Provider.Infrastructure.BackgroundJobs.RentalServices;
using CarRental.Provider.Persistence.Specifications.Customers;
using CarRental.Provider.Persistence.Specifications.Offers;
using FluentValidation;
using Hangfire;
using MediatR;

namespace CarRental.Provider.API.Requests.Offers.Handlers;

public class ChooseOfferCommandHandler : IRequestHandler<ChooseOfferCommand, Result<RentalDto>>
{
    private readonly IRepositoryBase<Offer> offersRepository;
    private readonly IRepositoryBase<Customer> customerRepository;
    private readonly IMapper mapper;
    private readonly ILogger<ChooseOfferCommandHandler> logger;
    private readonly IValidator<ChooseOfferCommand> validator;
    private readonly IBackgroundJobClient backgroundJobClient;
    private readonly IDateTimeProvider dateTimeProvider;

    public ChooseOfferCommandHandler(
        IRepositoryBase<Offer> offersRepository,
        IRepositoryBase<Customer> customersRepository,
        IMapper mapper,
        ILogger<ChooseOfferCommandHandler> logger,
        IValidator<ChooseOfferCommand> validator,
        IBackgroundJobClient backgroundJobClient,
        IDateTimeProvider dateTimeProvider)
    {
        this.offersRepository = offersRepository;
        this.customerRepository = customersRepository;
        this.mapper = mapper;
        this.logger = logger;
        this.validator = validator;
        this.backgroundJobClient = backgroundJobClient;
        this.dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<RentalDto>> Handle(ChooseOfferCommand request, CancellationToken cancellationToken)
    {
        var validation = await this.validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
        {
            return Result<RentalDto>.Invalid(validation.AsErrors());
        }

        var offerSpecification = new OfferByIdWithRentalSpecification(request.Id);
        var offer = await this.offersRepository.FirstOrDefaultAsync(offerSpecification, cancellationToken);

        if (offer == null)
        {
            this.logger.LogInformation($"Offer with Id = {request.Id} does not exist.");
            return Result<RentalDto>.NotFound();
        }

        if (offer.Rental != null)
        {
            this.logger.LogInformation($"Offer with Id = {request.Id} already has an associated rental.");
            return Result<RentalDto>.Invalid(
                    new ValidationError(nameof(Offer.Rental), "Offer already has an associated rental.")
                );
        }

        var now = this.dateTimeProvider.UtcNow;

        if(now >= offer.ExpiresAt)
        {
            this.logger.LogInformation($"Offer with Id = {request.Id} has expired.");
            return Result<RentalDto>.Invalid(
                    new ValidationError(nameof(Offer.ExpiresAt), $"Offer has expired.")
                );
        }

        var customerSpecification = new CustomerByEmailAddressSpecification(request.CustomerDto.EmailAddress);
        var customer = await this.customerRepository.FirstOrDefaultAsync(customerSpecification, cancellationToken);

        if (customer == null)
        {
            customer = this.mapper.Map<Customer>(request.CustomerDto);
            await this.customerRepository.AddAsync(customer, cancellationToken);
            this.logger.LogInformation("Creating customer: {@Customer}", request.CustomerDto);
        }
        else
        {
            this.mapper.Map(request.CustomerDto, customer);
            await this.customerRepository.UpdateAsync(customer, cancellationToken);
        }

        await this.customerRepository.SaveChangesAsync(cancellationToken);

        var rental = new Rental
        {
            Offer = offer,
            Customer = customer,
            Status = RentalStatus.Unconfirmed,
        };

        offer.Rental = rental;

        await this.offersRepository.UpdateAsync(offer, cancellationToken);
        await this.offersRepository.SaveChangesAsync(cancellationToken);

        var delay = offer.ExpiresAt - now;

        // send email here...

        this.backgroundJobClient.Schedule<IRentalStatusCheckerService>(
            x => x.CheckAndUpdateRentalStatusAsync(rental.Id, CancellationToken.None),
            delay
        );

        this.logger.LogInformation($"Scheduled status check in {delay.TotalMinutes} minutes.");

        var rentalDto = this.mapper.Map<RentalDto>(rental);

        return Result<RentalDto>.Created(rentalDto);
    }
}