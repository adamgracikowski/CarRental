using CarRental.Comparer.Web.Requests.DTOs.Pagination;

namespace CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;

public sealed record RentalTransactionsForEmployeeDto(
	ICollection<RentalTransactionForEmployeeDto> RentalTransactions,
	PaginationInfoDto PaginationInfo
);