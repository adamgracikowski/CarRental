using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.Requests.Providers.Commands;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;
using MediatR;

namespace CarRental.Comparer.API.Requests.Providers.Handlers;

public sealed class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, Result<OfferDto>>
{
	private readonly IRepositoryBase<Provider> providersRepository;
	private readonly ILogger<CreateOfferCommandHandler> logger;
	private readonly ICarComparisonService carComparisonService;

	public CreateOfferCommandHandler(IRepositoryBase<Provider> providersRepository,
		ILogger<CreateOfferCommandHandler> logger,
		ICarComparisonService carComparisonService)
	{
		this.providersRepository = providersRepository;
		this.logger = logger;
		this.carComparisonService = carComparisonService;
	}

	public async Task<Result<OfferDto>> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
	{
		var provider = await this.providersRepository.GetByIdAsync(request.Id, cancellationToken);

		if (provider?.Name is null)
		{
			this.logger.LogWarning($"Provider with ID {request.Id} not found.");
			return Result<OfferDto>.NotFound();
		}

		var offerDto = await this.carComparisonService.CreateOfferAsync(provider.Name, request.CarId, request.CreateOfferDto, cancellationToken);

		if (offerDto is null)
		{
			return Result<OfferDto>.Error();
		}

		return Result<OfferDto>.Success(offerDto);
	}
}