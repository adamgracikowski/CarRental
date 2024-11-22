namespace CarRental.Comparer.Web.Requests.DTOs.Pagination;

public sealed record PaginationInfoDto(
	int PageNumber,
	int PageSize,
	int NumberOfPages
);