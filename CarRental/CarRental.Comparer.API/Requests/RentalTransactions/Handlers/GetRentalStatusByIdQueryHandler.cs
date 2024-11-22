using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarComparisons;
using MediatR;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using CarRental.Comparer.Persistence.Specifications.RentalTransactions;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Handlers;

public class GetRentalStatusByIdQueryHandler : IRequestHandler<GetRentalStatusByIdQuery, Result<RentalStatusDto>>
{
	private readonly IRepositoryBase<RentalTransaction> rentalsRepository;
	private readonly ILogger<GetRentalStatusByIdQuery> logger;
	private readonly ICarComparisonService carComparisonService;

	public GetRentalStatusByIdQueryHandler(IRepositoryBase<RentalTransaction> rentalsRepository,
		ILogger<GetRentalStatusByIdQuery> logger,
		ICarComparisonService carComparisonService)
	{
		this.rentalsRepository = rentalsRepository;
		this.logger = logger;
		this.carComparisonService = carComparisonService;
	}

	public async Task<Result<RentalStatusDto>> Handle(GetRentalStatusByIdQuery request, CancellationToken cancellationToken)
	{
		var specification = new RentalByIdWithProviderSpecification(request.rentalId);

		var rental = await rentalsRepository.FirstOrDefaultAsync(specification, cancellationToken);

		if (rental?.Provider?.Name is null)
		{
			logger.LogWarning($"RentalTransaction with id: {request.rentalId} does not match with valid provider");
			return Result<RentalStatusDto>.NotFound();
		}

		var rentalStatusDto = await carComparisonService.GetRentalStatusByIdAsync(rental.Provider.Name, int.Parse(rental.RentalOuterId), cancellationToken);

		if (rentalStatusDto is null)
		{
			return Result<RentalStatusDto>.Error();
		}

		return Result<RentalStatusDto>.Success(rentalStatusDto);
	}
}