using CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;

namespace CarRental.Comparer.Web.Requests.RentalTransactionServices;

public interface IRentalTransactionService
{
	Task<RentalTransactionsForEmployeeDto?> GetRentalTransactionsByStatusAsync(string status, int page, int size, CancellationToken cancellationToken = default);

	Task<bool> AcceptReturnAsync(int id, AcceptRentalReturnDto acceptReturnDto, CancellationToken cancellationToken = default);

	Task<bool> ReturnRentalAsync(int id, CancellationToken cancellationToken = default);
}