using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

namespace CarRental.Comparer.Web.Requests.ReportServices;

public sealed class ReportService : IReportService
{
	private readonly HttpClient httpClient;
	private readonly IJSRuntime jsRuntime;

	public ReportService(HttpClient httpClient, IJSRuntime jsRuntime)
	{
		this.httpClient = httpClient;
		this.jsRuntime = jsRuntime;
	}

	public async Task<bool> GenerateReportAsync(string fileName, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default)
	{
		try
		{
			var requestContent = new StringContent(
				JsonSerializer.Serialize(new
				{
					DateFrom = dateFrom,
					DateTo = dateTo,
					Format = "excel"
				}),
				Encoding.UTF8,
				"application/json"
			);

			var response = await this.httpClient.PostAsync("RentalTransactions/Reports", requestContent, cancellationToken);

			if (!response.IsSuccessStatusCode)
			{
				return false;
			}

			var fileStream = response.Content.ReadAsStream(cancellationToken);

			using var streamReference = new DotNetStreamReference(stream: fileStream);

			await this.jsRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamReference);

			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			return false;
		}
	}
}