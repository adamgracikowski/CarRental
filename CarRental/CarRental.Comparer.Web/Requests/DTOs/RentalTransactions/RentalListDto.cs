using CarRental.Comparer.Web.Requests.DTOs.Pagination;

namespace CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;

public sealed record RentalListDto(
	PaginationInfoDto Pagination,
	ICollection<RentalDisplayDto> RentalList
);