using CarRental.Comparer.Web.Requests.Users.DTOs;
using System.Net.Http;

namespace CarRental.Comparer.Web.Requests.Users.UserApiService;

public interface IUserApiService
{
    Task<bool> CreateUserAsync(UserDto user);

    Task<UserDto?> GetUserByEmailAsync(string email);

    Task<bool> DeleteUserByEmailAsync(string email);
}
