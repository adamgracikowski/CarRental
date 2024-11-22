using CarRental.Comparer.API.Pagination;

namespace CarRental.Comparer.API.DTOs.RentalTransactions;

public sealed class RentalTransactionPaginatedListDto
{
	public PaginationInfo Pagination { get; set; }
	public ICollection<RentalTransactionDto> RentalList { get; set; }

	public RentalTransactionPaginatedListDto(ICollection<RentalTransactionDto> rentalList, PaginationInfo pagination)
	{
		RentalList = rentalList;
		Pagination = pagination;
	}
}