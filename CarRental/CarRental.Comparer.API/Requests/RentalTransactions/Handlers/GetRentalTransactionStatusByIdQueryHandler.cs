using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.Infrastructure.CarComparisons;
using MediatR;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using CarRental.Comparer.Persistence.Specifications.RentalTransactions;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;
using Hangfire;
using CarRental.Common.Core.Enums;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Handlers;

public class GetRentalTransactionStatusByIdQueryHandler : IRequestHandler<GetRentalTransactionStatusByIdQuery, Result<RentalTransactionStatusDto>>
{
	private readonly IRepositoryBase<RentalTransaction> rentalTransactionsRepository;
	private readonly ILogger<GetRentalTransactionStatusByIdQuery> logger;
	private readonly ICarComparisonService carComparisonService;

    public GetRentalTransactionStatusByIdQueryHandler(IRepositoryBase<RentalTransaction> rentalTransactionsRepository,
		ILogger<GetRentalTransactionStatusByIdQuery> logger,
		ICarComparisonService carComparisonService)
	{
		this.rentalTransactionsRepository = rentalTransactionsRepository;
		this.logger = logger;
		this.carComparisonService = carComparisonService;
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

		// enum service here
		//rentalTransaction.Status = rentalStatusDto.status == RentalStatus.Active.ToString() ? RentalStatus.Active : RentalStatus.Rejected; //demo
		
		var rentalTransactionStatusDto = new RentalTransactionStatusDto(rentalTransaction.Id, rentalTransaction.Status.ToString());

        await rentalTransactionsRepository.UpdateAsync(rentalTransaction, cancellationToken);
        await rentalTransactionsRepository.SaveChangesAsync(cancellationToken);

        return Result<RentalTransactionStatusDto>.Success(rentalTransactionStatusDto);
	}
}