using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;
using CarRental.Comparer.API.Requests.RentalTransactions.Commands;
using CarRental.Comparer.Infrastructure.Reports;
using CarRental.Comparer.Persistence.Specifications.Employees;
using CarRental.Comparer.Persistence.Specifications.RentalTransactions;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Handlers;

public class GenerateReportCommandHandler : IRequestHandler<GenerateReportCommand, Result<ReportResult>>
{
	private readonly IPeriodicReportService reportService;
	private readonly IRepositoryBase<Employee> employeesRepository;
	private readonly IRepositoryBase<RentalTransaction> rentalTransactionsRepository;

	public GenerateReportCommandHandler(
		IPeriodicReportService reportService,
		IRepositoryBase<Employee> employeesRepository,
		IRepositoryBase<RentalTransaction> rentalTransactionsRepository)
	{
		this.reportService = reportService;
		this.employeesRepository = employeesRepository;
		this.rentalTransactionsRepository = rentalTransactionsRepository;
	}

	public async Task<Result<ReportResult>> Handle(GenerateReportCommand request, CancellationToken cancellationToken)
	{
		var employeeSpecification = new EmployeeByEmailWithProviderSpecification(request.Email!);

		var employee = await this.employeesRepository.FirstOrDefaultAsync(employeeSpecification, cancellationToken);

		if (employee == null)
		{
			return Result<ReportResult>.Forbidden($"{request.Email} is not a valid employee's email address.");
		}

		var specification = new RentalTransactionByStatusProviderIdDateTimeRangeWithCarDetailsSpecification(
			request.GenerateReportDto.DateFrom,
			request.GenerateReportDto.DateTo,
			RentalStatus.Returned,
			employee.ProviderId
		);

		var rentalTransactions = await this.rentalTransactionsRepository.ListAsync(specification, cancellationToken);

		var dateFromFormatted = request.GenerateReportDto.DateFrom.ToString("d");
		var dateToFormatted = request.GenerateReportDto.DateTo.ToString("d");
		var reportName = $"CarRental_Report_{dateFromFormatted}_{dateToFormatted}.xlsx";

		var report = await this.reportService.GenerateReportAsync(
			rentalTransactions,
			reportName,
			cancellationToken
		);

		if (report is null)
		{
			return Result<ReportResult>.Error(
				new ErrorList(["An error occurred while generating the report. Please try again later."])
			);
		}

		return Result<ReportResult>.Success(report);
	}
}