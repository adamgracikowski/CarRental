namespace CarRental.Comparer.Web.Extensions.DateExtensions;

public sealed class DateTimeProvider : IDateTimeProvider
{
	public DateTime UtcNow => DateTime.UtcNow;
}