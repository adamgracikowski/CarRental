using Ardalis.Result;
using Ardalis.Specification;
using FluentValidation;
using CarRental.Comparer.Persistence.Specifications.RentalTransactions;
using MediatR;
using Ardalis.Result.FluentValidation;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Comparer.Persistence.Specifications.Employees;
using CarRental.Comparer.API.Requests.RentalTransactions.Commands;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Handlers;

public sealed class AcceptRentalReturnCommandHandler : IRequestHandler<AcceptRentalReturnCommand, Result>
{
	private readonly IValidator<AcceptRentalReturnCommand> validator;
	private readonly ICarComparisonService carComparisonService;
	private readonly IRepositoryBase<RentalTransaction> rentalTransactionsRepository;
	private readonly IRepositoryBase<Employee> employeesRepository;
	private readonly ILogger<AcceptRentalReturnCommandHandler> logger;
	private readonly IDateTimeProvider dateTimeProvider;

	public AcceptRentalReturnCommandHandler(
		IValidator<AcceptRentalReturnCommand> validator,
		ICarComparisonService carComparisonService,
		IRepositoryBase<RentalTransaction> rentalTransactionsRepository,
		ILogger<AcceptRentalReturnCommandHandler> logger,
		IDateTimeProvider dateTimeProvider,
		IRepositoryBase<Employee> employeesRepository)
	{
		this.validator = validator;
		this.carComparisonService = carComparisonService;
		this.rentalTransactionsRepository = rentalTransactionsRepository;
		this.logger = logger;
		this.dateTimeProvider = dateTimeProvider;
		this.employeesRepository = employeesRepository;
	}

	public async Task<Result> Handle(AcceptRentalReturnCommand request,
		CancellationToken cancellationToken)
	{
		var validation = await validator.ValidateAsync(request, cancellationToken);

		if (!validation.IsValid)
		{
			return Result.Invalid(validation.AsErrors());
		}

		var employeeSpecification = new EmployeeByEmailSpecification(request.EmployeeEmail!);

		var employee = await employeesRepository.FirstOrDefaultAsync(employeeSpecification);

		if (employee == null)
		{
			logger.LogWarning($"Employee with email {request.EmployeeEmail} not found.");
			return Result.Forbidden();
		}

		var specification = new RentalByIdWithProviderSpecification(request.Id);

		var rental = await rentalTransactionsRepository.FirstOrDefaultAsync(specification);

		if (rental == null)
		{
			logger.LogWarning($"Rental transaction with id {request.Id} not found.");
			return Result.NotFound();
		}

		if (employee.ProviderId != rental.ProviderId)
		{
			logger.LogWarning($"Employee's provider does not match with rental's provider.");
			return Result.Forbidden();
		}

		if (rental.Status == RentalStatus.Returned)
		{
			return Result.Success();
		}

		if (rental.Status != RentalStatus.ReadyForReturn)
		{
			logger.LogWarning($"Rental transaction with id {request.Id} is in {rental.Status.ToString()} state.");
			return Result.Invalid();
		}

		var providerResponse = await carComparisonService.AcceptRentalReturnAsync(
            rental.Provider.Name, 
            rental.RentalOuterId, 
            request.AcceptRentalReturn, 
            cancellationToken
        );

		if (providerResponse == null)
		{
			logger.LogWarning($"Error at CarComparisonService");
			return Result.Error();
		}

		rental.Status = RentalStatus.Returned;
		rental.Description = request.AcceptRentalReturn.Description;
		rental.Image = providerResponse.Image;
		rental.ReturnedAt = dateTimeProvider.UtcNow;

		await rentalTransactionsRepository.SaveChangesAsync();

		return Result.Success();
	}
}