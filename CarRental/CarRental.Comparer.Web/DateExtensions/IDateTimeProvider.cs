namespace CarRental.Comparer.Web.DateExtensions;

public interface IDateTimeProvider
{
	DateTime UtcNow { get; }
}