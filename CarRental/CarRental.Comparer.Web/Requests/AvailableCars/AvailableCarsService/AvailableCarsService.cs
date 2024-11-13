using CarRental.Comparer.Web.Requests.AvailableCars.DTOs;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Requests.AvailableCars.AvailableCarsService;

public class AvailableCarsService : IAvailableCarsService
{
    private readonly HttpClient _httpClient;

    public AvailableCarsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public class AuthRequest
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }  
    }

    public async Task<MakeListDto?> GetAvailableCars()
    {
        try
        {
            var authRequest = new AuthRequest
            {
                clientId = "CarRental.Comparer.Web",
                clientSecret = "435abef466399a06c0d313c8e9a1006914524d1ca6f1678b6338b68321ff806f6440acfe92421d864c294c583b47b354a3a38501725cd6ba2df7b725f5d0e3a3"
            };

            var authResponse = await _httpClient.PostAsJsonAsync("Auth/token", authRequest);
            
            if (!authResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var authData = await authResponse.Content.ReadFromJsonAsync<AuthResponse>();
            if (authData?.Token == null)
            {
                return null;
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authData.Token);

            return await _httpClient.GetFromJsonAsync<MakeListDto>("Cars/Available");
        }
        catch (Exception)
        {
            return null;
        }
    }
}
