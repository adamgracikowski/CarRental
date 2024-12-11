namespace CarRental.Comparer.Infrastructure.Reports;

public sealed record ReportResult(
	string ReportName,
	string ContentType,
	byte[] ReportContents
);