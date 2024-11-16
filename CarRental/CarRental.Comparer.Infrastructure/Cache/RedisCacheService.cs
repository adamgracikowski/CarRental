using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Microsoft.Extensions.Options;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarRental.Comparer.Infrastructure.Cache;

public sealed class RedisCacheService : ICacheService
{
    private readonly IDistributedCache cache;
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly RedisOptions redisOptions;

    public RedisCacheService(IDistributedCache cache, IDateTimeProvider dateTimeProvider, IOptions<RedisOptions> redisOptions)
    {
        this.cache = cache;
        this.dateTimeProvider = dateTimeProvider;
        this.redisOptions = redisOptions.Value;
    }

    public async Task AddDataAsync<T>(string key, T data) where T : class
    {
        var serializedObject = JsonSerializer.Serialize<T>(data);

        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(redisOptions.ExpirationTimeInMinutes)
        };

        await cache.SetStringAsync(key, serializedObject, options);
    }

    public async Task AddDataAsync<T>(string key, T data, DateTime expirationTime) where T : class
    {
        var serializedObject = JsonSerializer.Serialize<T>(data);

        var dateTimeNow = dateTimeProvider.UtcNow;

        var redisOptions = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = expirationTime - dateTimeNow
        };

        await cache.SetStringAsync(key, serializedObject, redisOptions);
    }

    public async Task<T?> GetDataByKeyAsync<T>(string key) where T : class
    {
        var serializedObject = await cache.GetStringAsync(key);

        if (serializedObject == null)
        {
            return null;
        }

        return JsonSerializer.Deserialize<T>(serializedObject);
    }
}
