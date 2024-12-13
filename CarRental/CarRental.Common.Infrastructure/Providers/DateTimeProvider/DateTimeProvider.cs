namespace CarRental.Common.Infrastructure.Providers.DateTimeProvider;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;
}