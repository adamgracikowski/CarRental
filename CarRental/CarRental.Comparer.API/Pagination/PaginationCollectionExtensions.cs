namespace CarRental.Comparer.API.Pagination;

public static class PaginationCollectionExtensions
{
	public static int NumberOfPages<T>(this ICollection<T> collection, int pageSize)
	{
		return (int)Math.Ceiling((double)collection.Count / pageSize);
	}

	public static ICollection<T> GetPage<T>(this ICollection<T> collection, int pageSize, int pageNumber)
	{
		var skip = (pageNumber - 1) * pageSize;
		return collection
			.Skip(skip)
			.Take(pageSize)
			.ToList();
	}
}