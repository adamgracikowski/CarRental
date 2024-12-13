using CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;
using CarRental.Comparer.Web.Requests.DTOs.Users;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Requests.UserServices;

public class UserService : IUserService
{
	private const string Users = "users";
	private const string RentalTransactions = "rental-transactions";

	private const int PageSize = 5;

	private readonly HttpClient httpClient;

	public UserService(HttpClient httpClient)
	{
		this.httpClient = httpClient;
	}

	public async Task<bool> CreateUserAsync(UserDto user, CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await httpClient.PostAsJsonAsync(Users, user, cancellationToken);
			return response.IsSuccessStatusCode;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public async Task<UserDto?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await httpClient.GetFromJsonAsync<UserDto>($"{Users}/{email}", cancellationToken);
			return response;
		}
		catch (Exception)
		{
			return null;
		}
	}
	public async Task<bool> DeleteUserByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await httpClient.DeleteAsync($"{Users}/{email}", cancellationToken);
			return response.IsSuccessStatusCode;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public async Task<bool> EditUserByEmailAsync(string email, UserDto user, CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await httpClient.PutAsJsonAsync($"{Users}/{email}", user, cancellationToken);
			return response.IsSuccessStatusCode;
		}
		catch (Exception)
		{
			return false;
		}
	}


	public async Task<RentalTransactionListDto?> GetRentalTransactionsByStatusAsync(string email, string status, int page, CancellationToken cancellationToken = default)
	{
		try
		{
			var url = $"{Users}/{email}/{RentalTransactions}/{status}?page={page}&size={PageSize}";
			var response = await httpClient.GetFromJsonAsync<RentalTransactionListDto>(url, cancellationToken);
			return response;
		}
		catch (Exception)
		{
			return null;
		}
	}
}