using CarRental.Comparer.Web.Requests.Users.DTOs;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Requests.Users.UserApiService;

public class UserApiService : IUserApiService
{
    private readonly HttpClient _httpClient;

    public UserApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CreateUserAsync(UserDto user)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("Users", user);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"Users/{email}");
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public async Task<bool> DeleteUserByEmailAsync(string email)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"Users/{email}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
