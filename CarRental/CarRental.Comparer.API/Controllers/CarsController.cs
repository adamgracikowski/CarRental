using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Comparer.API.Controllers;

[Route("cars")]
[ApiController]
public sealed class CarsController : ControllerBase
{
	private readonly ICarComparisonService carComparisonService;

	public CarsController(ICarComparisonService carComparisonService)
	{
		this.carComparisonService = carComparisonService;
	}

	/// <summary>
	/// Retrieves a list of all available cars from the comparison service.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing a unified list of available cars.</returns>
	/// <response code="200">The list of available cars was retrieved successfully.</response>
	/// <response code="500">An internal server error occurred while retrieving the cars.</response>
	[TranslateResultToActionResult]
	[HttpGet("available")]
	public async Task<Result<UnifiedCarListDto>> GetAvailableCars(CancellationToken cancellationToken)
	{
		var unifiedCarListDto = await this.carComparisonService.GetAllAvailableCarsAsync(cancellationToken);

		return Result<UnifiedCarListDto>.Success(unifiedCarListDto);
	}
}