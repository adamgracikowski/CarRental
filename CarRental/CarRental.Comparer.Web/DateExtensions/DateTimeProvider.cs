namespace CarRental.Comparer.Web.DateExtensions;

public sealed class DateTimeProvider : IDateTimeProvider
{
	public DateTime UtcNow => DateTime.UtcNow;
}