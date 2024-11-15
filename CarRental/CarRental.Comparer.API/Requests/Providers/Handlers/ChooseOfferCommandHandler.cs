using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.Requests.Providers.Commands;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using MediatR;

namespace CarRental.Comparer.API.Requests.Providers.Handlers;

public class ChooseOfferCommandHandler : IRequestHandler<ChooseOfferCommand, Result<RentalIdDto>>
{
    private readonly IRepositoryBase<Provider> providersRepository;
    private readonly ILogger<ChooseOfferCommandHandler> logger;
    private readonly ICarComparisonService carComparisonService;

    public ChooseOfferCommandHandler(IRepositoryBase<Provider> providersRepository,
        ILogger<ChooseOfferCommandHandler> logger,
        ICarComparisonService carComparisonService)
    {
        this.providersRepository = providersRepository;
        this.logger = logger;
        this.carComparisonService = carComparisonService;
    }

    public async Task<Result<RentalIdDto>> Handle(ChooseOfferCommand request, CancellationToken cancellationToken)
    {
        var provider = await providersRepository.GetByIdAsync(request.id, cancellationToken);

        if (provider?.Name is null)
        {
            logger.LogWarning($"Provider with ID {request.id} not found.");
            return Result<RentalIdDto>.NotFound();
        }

        var rentalIdDto = await carComparisonService.ChooseOfferAsync(provider.Name, request.offerId, request.chooseOfferDto, cancellationToken);

        if (rentalIdDto is null)
        {
            return Result<RentalIdDto>.Error();
        }

        return Result<RentalIdDto>.Success(rentalIdDto);
    }
}