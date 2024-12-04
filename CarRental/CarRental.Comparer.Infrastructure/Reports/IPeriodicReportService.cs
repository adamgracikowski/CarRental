using CarRental.Common.Core.ComparerEntities;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Comparer.Infrastructure.Reports;

public interface IPeriodicReportService
{
	Task<ReportResult?> GenerateReportAsync(IEnumerable<RentalTransaction> rentalTransactions, string reportName, CancellationToken cancellation);
}