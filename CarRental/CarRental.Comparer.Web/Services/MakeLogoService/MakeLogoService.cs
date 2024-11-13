using CarRental.Comparer.Web.Services.MakeLogoService.DTOs;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Services.MakeLogoService;

public class MakeLogoService : IMakeLogoService
{
    private readonly HttpClient _httpClient;

    private LogoListDto? _logos;

    private const string _fallbackUrl = "https://carrentalminisa.blob.core.windows.net/comparer-makes/placeholder.png";

    public MakeLogoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task RefreshLogos()
    {
        try
        {
            _logos = await _httpClient.GetFromJsonAsync<LogoListDto>("CarLogos");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            _logos = null;
        }
    }

    public string GetLogo(string make)
    {
        if (_logos == null)
        {
            return _fallbackUrl;
        }

        foreach (var logo in _logos.Logos)
        {
            if (logo.Make == make.ToLower())
            {
                return logo.LogoUrl;
            }
        }
        var placeHolder = _logos.Logos.Find(l => l.Make == "placeholder");

        return placeHolder.LogoUrl != null ? placeHolder.LogoUrl : _fallbackUrl;
    }
}
