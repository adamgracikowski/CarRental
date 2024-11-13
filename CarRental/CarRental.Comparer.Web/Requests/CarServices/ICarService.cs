using CarRental.Comparer.Web.Requests.DTOs.Makes;

namespace CarRental.Comparer.Web.Requests.CarServices;

public interface ICarService
{
    Task<MakeListDto> GetAvailableCars();
}