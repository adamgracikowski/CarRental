using CarRental.Comparer.Web.Requests.AvailableCars.DTOs;

namespace CarRental.Comparer.Web.Requests.AvailableCars.AvailableCarsService;

public interface IAvailableCarsService
{
    Task<MakeListDto?> GetAvailableCars();
}