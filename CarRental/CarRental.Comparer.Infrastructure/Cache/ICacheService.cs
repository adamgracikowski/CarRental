using CarRental.Common.Infrastructure.Providers.DateTimeProvider;

namespace CarRental.Comparer.Infrastructure.Cache;

public interface ICacheService
{
    Task<T?> GetDataByKeyAsync<T>(string key) where T : class;

    Task AddDataAsync<T>(string key, T data) where T : class;

    Task AddDataAsync<T>(string key, T data, DateTime dateTime) where T : class;
}