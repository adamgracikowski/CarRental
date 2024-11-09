using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Provider.API.DTOs.RentalReturns;
using CarRental.Provider.API.Requests.Rentals.Commands;
using CarRental.Provider.Infrastructure.Storages.BlobStorage;
using CarRental.Provider.Persistence.Specifications.Rentals;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;

namespace CarRental.Provider.API.Requests.Rentals.Handlers;

public sealed class AcceptRentalReturnCommandHandler : IRequestHandler<AcceptRentalReturnCommand, Result<RentalReturnDto>>
{
    private readonly IRepositoryBase<Rental> rentalsRepository;
    private readonly IRepositoryBase<RentalReturn> rentalReturnsRepository;
    private readonly IValidator<AcceptRentalReturnCommand> validator;
    private readonly IBlobStorageService blobStorageService;
    private readonly ILogger<AcceptRentalReturnCommandHandler> logger;
    private readonly IMapper mapper;
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly BlobContainersOptions options;

    public AcceptRentalReturnCommandHandler(
        IRepositoryBase<Rental> rentalsRepository,
        IRepositoryBase<RentalReturn> rentalReturnsRepository,
        IValidator<AcceptRentalReturnCommand> validator,
        IBlobStorageService blobStorageService,
        ILogger<AcceptRentalReturnCommandHandler> logger,
        IMapper mapper,
        IDateTimeProvider dateTimeProvider,
        IOptions<BlobContainersOptions> options)
    {
        this.rentalsRepository = rentalsRepository;
        this.rentalReturnsRepository = rentalReturnsRepository;
        this.validator = validator;
        this.blobStorageService = blobStorageService;
        this.logger = logger;
        this.mapper = mapper;
        this.dateTimeProvider = dateTimeProvider;
        this.options = options.Value;
    }

    public async Task<Result<RentalReturnDto>> Handle(AcceptRentalReturnCommand request, CancellationToken cancellationToken)
    {
        var validation = await this.validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
        {
            return Result<RentalReturnDto>.Invalid(validation.AsErrors());
        }

        var specification = new RentalByIdWithRentalReturnOfferCarSpecification(request.Id);

        var rental = await this.rentalsRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (rental == null)
        {
            return Result<RentalReturnDto>.NotFound();
        }

        if(rental.Status == RentalStatus.Returned)
        {
            return Result<RentalReturnDto>.Created(this.mapper.Map<RentalReturnDto>(rental.RentalReturn));
        }

        if(rental.Status != RentalStatus.ReadyForReturn)
        {
            return Result<RentalReturnDto>.Invalid(
                    new ValidationError(nameof(Rental.Status), $"Rental is not in the {nameof(RentalStatus.ReadyForReturn)} state.")
                );
        }

        var now = this.dateTimeProvider.UtcNow;

        var rentalReturn = new RentalReturn
        {
            Rental = rental,
            ReturnedAt = now,
            Description = request.AcceptRentalReturnDto.Description,
            Latitude = request.AcceptRentalReturnDto.Latitude,
            Longitude = request.AcceptRentalReturnDto.Longitude
        };

        if(request.AcceptRentalReturnDto.Image != null)
        {
            var fileName = await UploadImageAsync(request.AcceptRentalReturnDto.Image, rental.Id, cancellationToken);
            
            if(fileName == null)
            {
                this.logger.LogInformation("Image upload failed.");
                return Result<RentalReturnDto>.Invalid(
                    new ValidationError(nameof(RentalReturn.Image), "Image upload failed")
                );
            }

            rentalReturn.Image = fileName;
        }
        
        await this.rentalReturnsRepository.AddAsync(rentalReturn, cancellationToken);
        await this.rentalReturnsRepository.SaveChangesAsync(cancellationToken);

        rental.Status = RentalStatus.Returned;
        rental.RentalReturn = rentalReturn;
        rental.RentalReturnId = rentalReturn.Id;
        rental.Offer.Car.Status = CarStatus.Available;

        await this.rentalsRepository.UpdateAsync(rental, cancellationToken);
        await this.rentalsRepository.SaveChangesAsync(cancellationToken);

        // send email here...

        var rentalReturnDto = this.mapper.Map<RentalReturnDto>(rentalReturn);
        return Result<RentalReturnDto>.Created(rentalReturnDto);
    }

    private async Task<string?> UploadImageAsync(IFormFile image, int rentalId, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(image.FileName);
        var fileName = $"{rentalId}{extension}";

        var result = await this.blobStorageService.UploadFileAsync(
            options.RentalReturnsContainer,
            fileName,
            image,
            cancellationToken
        );

        return result ? fileName : null;
    }
}