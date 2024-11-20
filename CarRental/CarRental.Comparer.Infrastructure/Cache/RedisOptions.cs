namespace CarRental.Comparer.Infrastructure.Cache;

public sealed class RedisOptions
{
    public const string SectionName = "Redis";

    public string ConnectionString { get; set; } = string.Empty;

    public int ExpirationTimeInMinutes { get; set; }

    public string InstanceName {  get; set; } = string.Empty;

    public bool UseRedis {  get; set; }
}