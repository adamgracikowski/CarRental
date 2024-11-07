namespace CarRental.Common.Infrastructure.Providers.DateTimeProvider;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}