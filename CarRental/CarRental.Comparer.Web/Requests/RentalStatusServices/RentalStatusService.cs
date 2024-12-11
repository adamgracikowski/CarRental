using MudBlazor;

namespace CarRental.Comparer.Web.Requests.RentalStatusServices;

public class RentalStatusService : IRentalStatusService
{
	private static readonly string Unconfirmed = "Unconfirmed";
	private static readonly string Rejected = "Rejected";
    private static readonly string Active = "Active";
    private static readonly string ReadyForReturn = "Ready For Return";
    private static readonly string Returned = "Returned";

	private static readonly Dictionary<string, Color> StatusColors = new()
	{
		{ nameof(Unconfirmed), Color.Secondary },
		{ nameof(Rejected), Color.Error },
		{ nameof(Active), Color.Success },
		{ nameof(ReadyForReturn), Color.Warning },
		{ nameof(Returned), Color.Info },
	};

	private static readonly Dictionary<string, string> StatusDisplayNames = new()
	{
		{ nameof(Unconfirmed), Unconfirmed },
		{ nameof(Rejected), Rejected },
		{ nameof(Active), Active },
		{ nameof(ReadyForReturn), ReadyForReturn },
		{ nameof(Returned), Returned },
	};

	public Color ConvertToColor(string rentalStatus)
	{
		if(!StatusColors.TryGetValue(rentalStatus, out var color))
		{
			return Color.Default;
		}

		return color;
	}

	public string ConvertToDisplayName(string rentalStatus)
	{
		if (!StatusDisplayNames.TryGetValue(rentalStatus, out var displayName))
		{
			return string.Empty;
		}

		return displayName;
	}
}