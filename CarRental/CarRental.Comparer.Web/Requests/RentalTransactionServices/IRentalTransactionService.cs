using CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;
namespace CarRental.Comparer.Web.Requests.RentalTransactionService;

public interface IRentalTransactionService
{
	Task<RentalListDto?> GetUsersRentalsByStatusPaginated(string email, string status, int pageNumber);
}