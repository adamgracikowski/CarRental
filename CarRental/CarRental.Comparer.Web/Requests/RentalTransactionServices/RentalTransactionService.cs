using System.Net.Http.Json;
using CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;

namespace CarRental.Comparer.Web.Requests.RentalTransactionService;

public sealed class RentalTransactionService : IRentalTransactionService
{
	private readonly HttpClient httpClient;
	private readonly int pageSize = 5;
	private const string RentalTransactions = "RentalTransactions";

	public RentalTransactionService(HttpClient httpClient)
	{
		this.httpClient = httpClient;
	}

	public async Task<RentalTransactionListDto?> GetUsersRentalsByStatusPaginated(string email, string status, int pageNumber)
	{
		try
		{
			return await httpClient.GetFromJsonAsync<RentalTransactionListDto>($"{RentalTransactions}/{email}/{status}?page={pageNumber}&size={pageSize}");
		}
		catch (Exception)
		{
			return null;
		}
	}
}