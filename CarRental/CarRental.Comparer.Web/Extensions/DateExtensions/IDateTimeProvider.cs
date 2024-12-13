namespace CarRental.Comparer.Web.Extensions.DateExtensions;

public interface IDateTimeProvider
{
	DateTime UtcNow { get; }
}