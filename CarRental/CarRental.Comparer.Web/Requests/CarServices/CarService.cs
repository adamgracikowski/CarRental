using CarRental.Comparer.Web.Requests.DTOs.Makes;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Requests.CarServices;

public class CarService : ICarService
{
    private readonly HttpClient _httpClient;

    public CarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MakeListDto> GetAvailableCars()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<MakeListDto>("Cars/Available") 
                ?? new MakeListDto([]);
        }
        catch (Exception)
        {
            return new MakeListDto([]);
        }
    }
}