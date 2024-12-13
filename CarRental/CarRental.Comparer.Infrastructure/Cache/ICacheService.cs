namespace CarRental.Comparer.Infrastructure.Cache;

public interface ICacheService
{
	Task<T?> GetDataByKeyAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;

	Task AddDataAsync<T>(string key, T data, CancellationToken cancellationToken = default) where T : class;

	Task AddDataAsync<T>(string key, T data, DateTime dateTime, CancellationToken cancellationToken = default) where T : class;

	Task AddDataAsync<T>(string key, T data, int minutes, CancellationToken cancellationToken = default) where T : class;
}