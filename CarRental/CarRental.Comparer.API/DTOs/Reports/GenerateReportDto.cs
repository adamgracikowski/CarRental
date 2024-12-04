namespace CarRental.Comparer.API.DTOs.Reports;

public sealed record GenerateReportDto(
	DateTime DateFrom,
	DateTime DateTo,
	string Format
);