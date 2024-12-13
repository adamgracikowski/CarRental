using CarRental.Common.Core.ComparerEntities;
using ClosedXML.Excel;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace CarRental.Comparer.Infrastructure.Reports.ExcelReports;

public sealed class ExcelReportService : IPeriodicReportService
{
	private const string MonthFormat = "MMMM";
	private const string Year = "Year";
	private const string Month = "Month";
	private const string Make = "Make";
	private const string Model = "Model";
	private const string NumberOfRentals = "Number of Rentals";
	private const string Revenue = "Revenue";

	private readonly ILogger<ExcelReportService> logger;
	private readonly FileExtensionContentTypeProvider fileExtensionContentTypeProvider;

	public ExcelReportService(
		ILogger<ExcelReportService> logger,
		FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
	{
		this.logger = logger;
		this.fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
	}

	public Task<ReportResult?> GenerateReportAsync(IEnumerable<RentalTransaction> rentalTransactions, string reportName, CancellationToken cancellation = default)
	{
		var groupedData = GroupReportData(rentalTransactions);

		using var workbook = new XLWorkbook();

		var dataRange = ConfigureReportTable(workbook, groupedData);

		ConfigurePivotTable(workbook, dataRange);

		using var memoryStream = new MemoryStream();

		workbook.SaveAs(memoryStream);

		try
		{
			memoryStream.Seek(0, SeekOrigin.Begin);
		}
		catch (Exception ex)
		{
			this.logger.LogWarning(ex.Message);
			return Task.FromResult<ReportResult?>(null);
		}

		if (!this.fileExtensionContentTypeProvider.TryGetContentType(reportName, out var contentType))
		{
			contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		}

		var reportResult = new ReportResult(
			ReportName: reportName,
			ContentType: contentType,
			ReportContents: memoryStream.ToArray()
		);

		return Task.FromResult<ReportResult?>(reportResult);
	}

	private IEnumerable<object> GroupReportData(IEnumerable<RentalTransaction> rentalTransactions)
	{
		var groupedData = rentalTransactions
			.GroupBy(r => r.RentedAt.Year)
			.SelectMany(yearGroup => yearGroup
				.GroupBy(r => r.RentedAt.ToString(MonthFormat, CultureInfo.InvariantCulture))
				.SelectMany(monthGroup => monthGroup
					.GroupBy(r => r.CarDetails.Make)
					.SelectMany(makeGroup => makeGroup
						.GroupBy(r => r.CarDetails.Model)
						.Select(modelGroup => new
						{
							Year = yearGroup.Key,
							Month = monthGroup.Key,
							Make = makeGroup.Key,
							Model = modelGroup.Key,
							NumberOfRentals = modelGroup.Count(),
							Revenue = modelGroup.Sum(r => CalculateRevenue(r))
						})
					)
				)
			)
			.ToList();

		return groupedData;
	}

	private IXLRange? ConfigureReportTable(XLWorkbook workbook, IEnumerable<object> groupedData)
	{
		var worksheet = workbook.AddWorksheet("Report");

		worksheet.Cell("A1").Value = Year;
		worksheet.Cell("B1").Value = Month;
		worksheet.Cell("C1").Value = Make;
		worksheet.Cell("D1").Value = Model;
		worksheet.Cell("E1").Value = NumberOfRentals;
		worksheet.Cell("F1").Value = Revenue;

		worksheet.Cell("A2").InsertData(groupedData);

		var dataRange = worksheet.RangeUsed();

		if (dataRange != null)
		{
			var table = dataRange.CreateTable();

			table.Theme = XLTableTheme.TableStyleMedium9;
			table.ShowAutoFilter = true;
			table.ShowTotalsRow = true;
			table.Field(NumberOfRentals).TotalsRowFunction = XLTotalsRowFunction.Sum;
			table.Field(Revenue).TotalsRowFunction = XLTotalsRowFunction.Sum;

			worksheet.Columns().AdjustToContents();
		}

		return dataRange;
	}

	private void ConfigurePivotTable(XLWorkbook workbook, IXLRange? dataRange)
	{
		var pivotSheet = workbook.Worksheets.Add("PivotTable");
		var pivotTable = pivotSheet.PivotTables.Add("PivotTable", pivotSheet.Cell("A1"), dataRange);

		pivotTable.RowLabels.Add(Year);
		pivotTable.RowLabels.Add(Month);
		pivotTable.RowLabels.Add(Make);
		pivotTable.RowLabels.Add(Model);

		pivotTable.Values.Add(NumberOfRentals).SetSummaryFormula(XLPivotSummary.Sum);
		pivotTable.Values.Add(Revenue).SetSummaryFormula(XLPivotSummary.Sum);

		pivotTable.ShowGrandTotalsColumns = true;
		pivotTable.ShowGrandTotalsRows = true;

		pivotSheet.Columns().AdjustToContents();
	}

	private decimal CalculateRevenue(RentalTransaction rentalTransaction)
	{
		if (rentalTransaction.ReturnedAt is null)
		{
			throw new ArgumentNullException($"This rental transaction has not been returned yet.");
		}

		var difference = rentalTransaction.ReturnedAt.Value - rentalTransaction.RentedAt;
		var days = (int)Math.Ceiling(difference.TotalDays);
		var insuranceRevenue = days * rentalTransaction.InsurancePricePerDay;
		var rentalRevenue = days * rentalTransaction.RentalPricePerDay;
		var totalRevenue = insuranceRevenue + rentalRevenue;

		return totalRevenue;
	}
}