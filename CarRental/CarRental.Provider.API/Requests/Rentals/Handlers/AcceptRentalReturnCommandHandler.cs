﻿using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Common.Infrastructure.Storages.BlobStorage;
using CarRental.Provider.API.DTOs.RentalReturns;
using CarRental.Provider.API.Requests.Rentals.Commands;
using CarRental.Provider.Infrastructure.Calculators.RentalBillCalculator;
using CarRental.Provider.Persistence.Specifications.Rentals;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using CarRental.Provider.Infrastructure.EmailServices;

namespace CarRental.Provider.API.Requests.Rentals.Handlers;

public sealed class AcceptRentalReturnCommandHandler : IRequestHandler<AcceptRentalReturnCommand, Result<RentalReturnDto>>
{
	private readonly IRepositoryBase<Rental> rentalsRepository;
	private readonly IRepositoryBase<RentalReturn> rentalReturnsRepository;
	private readonly IBlobStorageService blobStorageService;
	private readonly ILogger<AcceptRentalReturnCommandHandler> logger;
	private readonly IMapper mapper;
	private readonly IDateTimeProvider dateTimeProvider;
	private readonly IRentalBillCalculatorService rentalBillCalculatorService;
	private readonly BlobContainersOptions options;
	private readonly IEmailService emailService;
	private readonly IEmailInputMaker emailInputMaker;

	public AcceptRentalReturnCommandHandler(
		IRepositoryBase<Rental> rentalsRepository,
		IRepositoryBase<RentalReturn> rentalReturnsRepository,
		IBlobStorageService blobStorageService,
		ILogger<AcceptRentalReturnCommandHandler> logger,
		IMapper mapper,
		IDateTimeProvider dateTimeProvider,
		IRentalBillCalculatorService rentalBillCalculatorService,
		IOptions<BlobContainersOptions> options,
		IEmailInputMaker emailInputMaker,
		IEmailService emailService)
	{
		this.rentalsRepository = rentalsRepository;
		this.rentalReturnsRepository = rentalReturnsRepository;
		this.blobStorageService = blobStorageService;
		this.logger = logger;
		this.mapper = mapper;
		this.dateTimeProvider = dateTimeProvider;
		this.rentalBillCalculatorService = rentalBillCalculatorService;
		this.options = options.Value;
		this.emailService = emailService;
		this.emailInputMaker = emailInputMaker;
	}

	public async Task<Result<RentalReturnDto>> Handle(AcceptRentalReturnCommand request, CancellationToken cancellationToken)
	{
		var specification = new RentalByIdWithRentalReturnClientOfferCarModelMakeSpecification(request.Id);

		var rental = await this.rentalsRepository.FirstOrDefaultAsync(specification, cancellationToken);

		if (rental == null)
		{
			return Result<RentalReturnDto>.NotFound();
		}

		if (rental.Offer.GeneratedBy != request.Audience)
		{
			return Result<RentalReturnDto>.Forbidden("Offer associated with rental was generated by a different audience.");
		}

		if (rental.Status == RentalStatus.Returned)
		{
			return Result<RentalReturnDto>.Created(this.mapper.Map<RentalReturnDto>(rental.RentalReturn));
		}

		if (rental.Status != RentalStatus.ReadyForReturn)
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

		string? fileUrl = null;

		if (request.AcceptRentalReturnDto.Image != null)
		{
			var fileData = await UploadImageAsync(request.AcceptRentalReturnDto.Image, rental.Id, cancellationToken);

			if (fileData.FileUrl == null)
			{
				this.logger.LogInformation("Image upload failed.");
				return Result<RentalReturnDto>.Invalid(
					new ValidationError(nameof(RentalReturn.Image), "Image upload failed")
				);
			}

			rentalReturn.Image = fileData.FileName;
			fileUrl = fileData.FileUrl;
		}

		await this.rentalReturnsRepository.AddAsync(rentalReturn, cancellationToken);
		await this.rentalReturnsRepository.SaveChangesAsync(cancellationToken);

		rental.Status = RentalStatus.Returned;
		rental.RentalReturn = rentalReturn;
		rental.RentalReturnId = rentalReturn.Id;

		var car = rental.Offer.Car;

		car.Status = CarStatus.Available;
		car.Latitude = request.AcceptRentalReturnDto.Latitude;
		car.Longitude = request.AcceptRentalReturnDto.Longitude;

		await this.rentalsRepository.UpdateAsync(rental, cancellationToken);
		await this.rentalsRepository.SaveChangesAsync(cancellationToken);

		var emailInput = emailInputMaker.GenerateRentalReturnedTemplate(
			rental.Customer.EmailAddress,
			$"{rental.Customer.FirstName} {rental.Customer.LastName}",
			rental.Offer.Car.Model.Make.Name,
			rental.Offer.Car.Model.Name,
			rental.Offer.GeneratedAt,
			rentalReturn.ReturnedAt,
			rental.Offer.InsurancePricePerDay,
			rental.Offer.RentalPricePerDay
			);

		await emailService.SendEmailAsync(emailInput);

		var rentalReturnDto = this.mapper.Map<RentalReturnDto>(rentalReturn);
		rentalReturnDto.Image = fileUrl;

		return Result<RentalReturnDto>.Created(rentalReturnDto);
	}

	private async Task<(string FileName, string? FileUrl)> UploadImageAsync(IFormFile image, int rentalId, CancellationToken cancellationToken)
	{
		var extension = Path.GetExtension(image.FileName);
		var fileName = $"{rentalId}{extension}";

		var result = await this.blobStorageService.UploadFileAsync(
			options.RentalReturnsContainer,
			fileName,
			image,
			cancellationToken
		);

		return (fileName, result);
	}
}