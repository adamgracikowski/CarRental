using CarRental.Comparer.Web.Requests.DTOs.Pagination;

namespace CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;

public sealed record RentalTransactionListDto(
	PaginationInfoDto Pagination,
	ICollection<RentalTransactionDisplayDto> RentalList
);