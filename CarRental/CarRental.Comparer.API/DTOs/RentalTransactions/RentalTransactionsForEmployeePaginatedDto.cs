using CarRental.Comparer.API.Pagination;

namespace CarRental.Comparer.API.DTOs.RentalTransactions;

public sealed record RentalTransactionsForEmployeePaginatedDto(
	PaginationInfo PaginationInfo,
	ICollection<RentalTransactionForEmployeeDto> RentalTransactionForEmployeeDtos
);