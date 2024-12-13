using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;
using CarRental.Comparer.API.DTOs.RentalTransactions;
using CarRental.Comparer.API.Pagination;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using CarRental.Comparer.Persistence.Specifications.Employees;
using CarRental.Comparer.Persistence.Specifications.RentalTransactions;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Handlers;

public sealed class GetAllRentalTransactionsQueryHandler : IRequestHandler<GetRentalTransactionsForEmployeeQuery,
	Result<RentalTransactionsForEmployeePaginatedDto>>
{
	private readonly IMapper mapper;
	private readonly IRepositoryBase<Employee> employeesRepository;
	private readonly IRepositoryBase<RentalTransaction> rentalTransactionsRepository;
	public GetAllRentalTransactionsQueryHandler(
		IRepositoryBase<Employee> employeesRepotitory,
		IRepositoryBase<RentalTransaction> rentalTransactionsRepository,
		IMapper mapper)
	{
		this.employeesRepository = employeesRepotitory;
		this.rentalTransactionsRepository = rentalTransactionsRepository;
		this.mapper = mapper;
	}

	public async Task<Result<RentalTransactionsForEmployeePaginatedDto>> Handle(GetRentalTransactionsForEmployeeQuery request,
		CancellationToken cancellationToken)
	{
		var employeeSpecification = new EmployeeByEmailWithProviderSpecification(request.EmployeeEmail!);
		var employee = await this.employeesRepository.FirstOrDefaultAsync(employeeSpecification, cancellationToken);
		if (employee == null)
		{
			return Result.Forbidden($"{request.EmployeeEmail} is not a valid employee's email address.");
		}

		var status = Enum.Parse<RentalStatus>(request.Status, true);

		var rentalTransactionSpecification = new RentalTransactionByProviderByStatusSpecification(employee.ProviderId, status);

		var rentals = await this.rentalTransactionsRepository.ListAsync(rentalTransactionSpecification, cancellationToken);

		var paginationInfo = new PaginationInfo(request.Page, request.Size, rentals.NumberOfPages(request.Size));

		var rentalDtos = this.mapper.Map<List<RentalTransactionForEmployeeDto>>(rentals);

		var paginatedRentals = new RentalTransactionsForEmployeePaginatedDto(paginationInfo, rentalDtos.GetPage(request.Size, request.Page));

		return paginatedRentals;
	}
}