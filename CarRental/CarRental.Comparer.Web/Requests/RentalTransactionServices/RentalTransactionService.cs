using CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Requests.RentalTransactionServices;

public sealed class RentalTransactionService : IRentalTransactionService
{
	private const string RentalTransactions = "rental-transactions";
	private const string Latitude = "Latitude";
	private const string Longitude = "Longitude";

	private readonly HttpClient httpClient;

	public RentalTransactionService(HttpClient httpClient)
	{
		this.httpClient = httpClient;
	}

	public async Task<bool> AcceptReturnAsync(int id, AcceptRentalReturnDto acceptReturnDto, CancellationToken cancellationToken = default)
	{
		try
		{
			using var formContent = new MultipartFormDataContent();

			if (acceptReturnDto.Image != null)
			{
				var imageContent = new StreamContent(acceptReturnDto.Image.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024, cancellationToken: cancellationToken));

				imageContent.Headers.ContentType = new MediaTypeHeaderValue(acceptReturnDto.Image.ContentType);

				formContent.Add(imageContent, nameof(acceptReturnDto.Image), acceptReturnDto.Image.Name);
			}

			formContent.Add(new StringContent(acceptReturnDto.Description), nameof(acceptReturnDto.Description));
			formContent.Add(new StringContent(acceptReturnDto.Latitude.ToString(CultureInfo.InvariantCulture)), Latitude);
			formContent.Add(new StringContent(acceptReturnDto.Longitude.ToString(CultureInfo.InvariantCulture)), Longitude);

			var url = $"{RentalTransactions}/{id}/accept-return";

			var request = new HttpRequestMessage(HttpMethod.Patch, url)
			{
				Content = formContent
			};

			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = await this.httpClient.SendAsync(request, cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				return true;
			}

			var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
			Console.WriteLine($"Error response: {errorContent}");

			return false;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
			return false;
		}
	}

	public async Task<bool> ReturnRentalAsync(int id, CancellationToken cancellationToken = default)
	{
		try
		{
			var url = $"{RentalTransactions}/{id}/return";

			var response = await httpClient.PatchAsync(url, null);

			if (response.IsSuccessStatusCode)
			{
				return true;
			}

			return false;
		}
		catch
		{
			return false;
		}
	}

	public async Task<RentalTransactionsForEmployeeDto?> GetRentalTransactionsByStatusAsync(string status, int page, int size, CancellationToken cancellationToken = default)
	{
		try
		{
			var url = $"{RentalTransactions}/{status}?page={page}&size={size}";

			var response = await this.httpClient.GetFromJsonAsync<RentalTransactionsForEmployeeDto>(url, cancellationToken);

			return response;
		}
		catch (Exception)
		{
			return null;
		}
	}
}