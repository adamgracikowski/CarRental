using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;
using CarRental.Comparer.Infrastructure.CarProviders.RentalStatusConversions;
using CarRental.Comparer.Persistence.Specifications.RentalTransactions;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Handlers;

public class GetRentalTransactionStatusByIdQueryHandler : IRequestHandler<GetRentalTransactionStatusByIdQuery, Result<RentalTransactionStatusDto>>
{
	private readonly IRepositoryBase<RentalTransaction> rentalTransactionsRepository;
	private readonly ILogger<GetRentalTransactionStatusByIdQuery> logger;
	private readonly ICarComparisonService carComparisonService;
	private readonly IRentalStatusConverter rentalStatusConverter;

	public GetRentalTransactionStatusByIdQueryHandler(IRepositoryBase<RentalTransaction> rentalTransactionsRepository,
		ILogger<GetRentalTransactionStatusByIdQuery> logger,
		ICarComparisonService carComparisonService,
		IRentalStatusConverter rentalStatusConverter)
	{
		this.rentalTransactionsRepository = rentalTransactionsRepository;
		this.logger = logger;
		this.carComparisonService = carComparisonService;
		this.rentalStatusConverter = rentalStatusConverter;
	}

	public async Task<Result<RentalTransactionStatusDto>> Handle(GetRentalTransactionStatusByIdQuery request, CancellationToken cancellationToken)
	{
		var specification = new RentalByIdWithProviderSpecification(request.rentalTransactionId);

		var rentalTransaction = await rentalTransactionsRepository.FirstOrDefaultAsync(specification, cancellationToken);

		if (rentalTransaction?.Provider?.Name is null)
		{
			logger.LogWarning($"RentalTransaction with id: {request.rentalTransactionId} does not match with valid provider");
			return Result<RentalTransactionStatusDto>.NotFound();
		}

		var rentalStatusDto = await carComparisonService.GetRentalStatusByIdAsync(rentalTransaction.Provider.Name, rentalTransaction.RentalOuterId, cancellationToken);

		if (rentalStatusDto is null)
		{
			return Result<RentalTransactionStatusDto>.Error();
		}

		if (this.rentalStatusConverter.TryConvertFromProviderRentalStatus(rentalStatusDto.Status, rentalTransaction.Provider.Name, out var updatedStatus))
		{
			rentalTransaction.Status = updatedStatus;
			await rentalTransactionsRepository.UpdateAsync(rentalTransaction, cancellationToken);
			await rentalTransactionsRepository.SaveChangesAsync(cancellationToken);
		}

		var rentalTransactionStatusDto = new RentalTransactionStatusDto(rentalTransaction.Id, rentalTransaction.Status.ToString());

		return Result<RentalTransactionStatusDto>.Success(rentalTransactionStatusDto);
	}
}