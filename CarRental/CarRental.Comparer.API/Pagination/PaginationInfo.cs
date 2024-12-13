namespace CarRental.Comparer.API.Pagination;

public sealed record PaginationInfo(
	int PageNumber,
	int PageSize,
	int NumberOfPages
);