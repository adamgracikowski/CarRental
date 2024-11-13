using CarRental.Comparer.Web.Requests.DTOs.Users;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Requests.UserServices;

public class UserService : IUserService
{
    private const string Users = "Users";

    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CreateUserAsync(UserDto user)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(Users, user);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"{Users}/{email}");
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task<bool> DeleteUserByEmailAsync(string email)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{Users}/{email}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
}