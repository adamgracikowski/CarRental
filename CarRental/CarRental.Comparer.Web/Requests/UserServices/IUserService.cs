using CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;
using CarRental.Comparer.Web.Requests.DTOs.Users;

namespace CarRental.Comparer.Web.Requests.UserServices;

public interface IUserService
{
    Task<bool> CreateUserAsync(UserDto user, CancellationToken cancellationToken = default);

    Task<UserDto?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> DeleteUserByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> EditUserByEmailAsync(string email, UserDto user, CancellationToken cancellationToken = default);

	Task<RentalTransactionListDto?> GetRentalTransactionsByStatusAsync(string email, string status, int page, CancellationToken cancellationToken = default);
}