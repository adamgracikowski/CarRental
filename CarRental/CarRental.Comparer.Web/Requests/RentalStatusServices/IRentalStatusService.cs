using MudBlazor;

namespace CarRental.Comparer.Web.Requests.RentalStatusServices;

public interface IRentalStatusService
{
	string ConvertToDisplayName(string rentalStatus);
	Color ConvertToColor(string rentalStatus);
}
