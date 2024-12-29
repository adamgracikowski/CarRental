using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;
using CarRental.Comparer.API.Requests.RentalTransactions.Commands;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Persistence.Specifications.RentalTransactions;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Handlers;

public class ReturnRentalTransactionCommandHandler : IRequestHandler<ReturnRentalTransactionCommand, Result>
{
	private readonly IRepositoryBase<RentalTransaction> rentalTransactionsRepository;
	private readonly ILogger<ReturnRentalTransactionCommandHandler> logger;
	private readonly ICarComparisonService carComparisonService;

	public ReturnRentalTransactionCommandHandler(
		IRepositoryBase<RentalTransaction> rentalTransactionsRepository,
		ILogger<ReturnRentalTransactionCommandHandler> logger,
		ICarComparisonService carComparisonService)
	{
		this.rentalTransactionsRepository = rentalTransactionsRepository;
		this.logger = logger;
		this.carComparisonService = carComparisonService;
	}

	public async Task<Result> Handle(ReturnRentalTransactionCommand request, CancellationToken cancellationToken)
	{
		var rentalTransactionSpecification = new RentalTransactionByIdWithUserWithProviderSpecification(request.Id);

		var rentalTransaction = await this.rentalTransactionsRepository.FirstOrDefaultAsync(rentalTransactionSpecification);

		if (rentalTransaction == null)
		{
			this.logger.LogWarning($"Rental transaction with id {request.Id} not found.");
			return Result.Invalid();
		}

		if (request.Email != rentalTransaction.User.Email)
		{
			this.logger.LogWarning($"Email associated with rental transaction with id {request.Id} does not match with user's email.");
			return Result.Forbidden();
		}

		if (rentalTransaction.Status == RentalStatus.ReadyForReturn)
		{
			return Result.Success();
		}

		if (rentalTransaction.Status != RentalStatus.Active)
		{
			this.logger.LogWarning($"Rental transaction with id {request.Id} is not in active state.");
			return Result.Invalid();
		}

		var result = await this.carComparisonService.ReturnRentalAsync(rentalTransaction.Provider.Name, rentalTransaction.RentalOuterId, cancellationToken);

		if (result == null)
		{
			return Result.Error();
		}

		rentalTransaction.Status = RentalStatus.ReadyForReturn;
		await this.rentalTransactionsRepository.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}