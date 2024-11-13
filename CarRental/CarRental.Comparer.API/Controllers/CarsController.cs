using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Comparer.API.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class CarsController : ControllerBase
{
    private readonly ICarComparisonService carComparisonService;

    public CarsController(ICarComparisonService carComparisonService)
    {
        this.carComparisonService = carComparisonService;
    }

    [TranslateResultToActionResult]
    [HttpGet("Available")]
    public async Task<Result<UnifiedCarListDto>> GetAvailableCars(CancellationToken cancellationToken)
    {
        var unifiedCarListDto = await this.carComparisonService.GetAllAvailableCarsAsync(cancellationToken);

        return Result<UnifiedCarListDto>.Success(unifiedCarListDto);
    }
}