namespace CarRental.Comparer.Web.Requests.ReportServices;

public interface IReportService
{
	Task<bool> GenerateReportAsync(string fileName, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default);
}