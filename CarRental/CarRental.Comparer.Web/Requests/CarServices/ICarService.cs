using CarRental.Comparer.Web.Requests.DTOs.Makes;
using CarRental.Comparer.Web.Requests.DTOs.Models;
using CarRental.Comparer.Web.Requests.DTOs.Offers;
using System.Runtime.CompilerServices;

namespace CarRental.Comparer.Web.Requests.CarServices;

public interface ICarService
{
    Task<MakeListDto> GetAvailableCars(CancellationToken cancellationToken = default);

    Task<ModelDetailsDto?> GetModelDetailsAsync(string makeName, string modelName, IEqualityComparer<string> equalityComparer, CancellationToken cancellationToken = default);
}