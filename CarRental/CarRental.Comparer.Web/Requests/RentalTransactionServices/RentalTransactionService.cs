using CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Requests.RentalTransactionServices;

public sealed class RentalTransactionService : IRentalTransactionService
{
	private const string RentalTransactions = "RentalTransactions";

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
			formContent.Add(new StringContent(acceptReturnDto.Latitude.ToString()), nameof(acceptReturnDto.Latitude));
			formContent.Add(new StringContent(acceptReturnDto.Longitude.ToString()), nameof(acceptReturnDto.Longitude));

			var url = $"{RentalTransactions}/{id}/accept-return";

			var request = new HttpRequestMessage(HttpMethod.Patch, url)
			{
				Content = formContent
			};

			var response = await this.httpClient.SendAsync(request, cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				return true;
			}

			return false;
		}
		catch (Exception)
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