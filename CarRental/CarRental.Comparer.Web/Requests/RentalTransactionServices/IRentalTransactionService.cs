using CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;
namespace CarRental.Comparer.Web.Requests.RentalTransactionService;

public interface IRentalTransactionService
{
	Task<RentalTransactionListDto?> GetUsersRentalsByStatusPaginated(string email, string status, int pageNumber);
}