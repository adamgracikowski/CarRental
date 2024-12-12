using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;
using CarRental.Comparer.API.Requests.RentalTransactions.Commands;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Persistence.Specifications.RentalTransactions;
using FluentValidation;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Handlers;

public class ReturnRentalTransactionCommandHandler : IRequestHandler<ReturnRentalTransactionCommand, Result>
{
	private readonly IValidator<ReturnRentalTransactionCommand> validator;
	private readonly IRepositoryBase<RentalTransaction> rentalTransactionsRepository;
	private readonly ILogger<ReturnRentalTransactionCommandHandler> logger;
	private readonly ICarComparisonService carComparisonService;


	public ReturnRentalTransactionCommandHandler(
		IValidator<ReturnRentalTransactionCommand> validator,
		IRepositoryBase<RentalTransaction> rentalTransactionsRepository,
		ILogger<ReturnRentalTransactionCommandHandler> logger,
		ICarComparisonService carComparisonService)
	{
		this.validator = validator;
		this.rentalTransactionsRepository = rentalTransactionsRepository;
		this.logger = logger;
		this.carComparisonService = carComparisonService;
	}

	public async Task<Result> Handle(ReturnRentalTransactionCommand request, CancellationToken cancellationToken)
	{
		var validation = await validator.ValidateAsync(request);

		if (!validation.IsValid)
		{
			return Result.Invalid(validation.AsErrors());
		}

		var rentalTransactionSpecification = new RentalTransactionByIdWithUserWithProviderSpecification(request.Id);

		var rentalTransaction = await rentalTransactionsRepository.FirstOrDefaultAsync(rentalTransactionSpecification);

		if (rentalTransaction == null)
		{
			logger.LogWarning($"Rental transaction with id {request.Id} not found.");
			return Result.Invalid();
		}

		if (request.Email != rentalTransaction.User.Email)
		{
			logger.LogWarning($"Email associated with rental transaction with id {request.Id} does not match with user's email.");
			return Result.Forbidden();
		}

		if (rentalTransaction.Status == RentalStatus.ReadyForReturn)
		{
			return Result.Success();
		}

		if (rentalTransaction.Status != RentalStatus.Active)
		{
			logger.LogWarning($"Rental transaction with id {request.Id} is not in active state.");
			return Result.Invalid();
		}

		var result = await carComparisonService.ReturnRentalAsync(rentalTransaction.Provider.Name, rentalTransaction.RentalOuterId, cancellationToken);

		if (result == null)
		{
			return Result.Error();
		}

		rentalTransaction.Status = RentalStatus.ReadyForReturn;
		await rentalTransactionsRepository.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}