using CarRental.Comparer.Web.Requests.DTOs.Users;

namespace CarRental.Comparer.Web.Requests.UserServices;

public interface IUserService
{
    Task<bool> CreateUserAsync(UserDto user);

    Task<UserDto?> GetUserByEmailAsync(string email);

    Task<bool> DeleteUserByEmailAsync(string email);

    Task<bool> EditUserByEmailAsync(string email, UserDto user);
}