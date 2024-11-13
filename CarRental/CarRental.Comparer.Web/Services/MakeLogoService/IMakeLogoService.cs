namespace CarRental.Comparer.Web.Services.MakeLogoService;

public interface IMakeLogoService
{
    Task RefreshLogos();

    string GetLogo(string make);
}
