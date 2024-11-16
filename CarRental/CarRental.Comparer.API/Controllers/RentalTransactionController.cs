﻿using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Comparer.API.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class RentalTransactionsController : ControllerBase
{
    private readonly IMediator mediator;

    public RentalTransactionsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [TranslateResultToActionResult]
    [HttpGet("{id}/status")]
    public async Task<Result<RentalStatusDto>> GetRentalStatus(int id, CancellationToken cancellationToken)
    {
        var query = new GetRentalStatusByIdQuery(id);

        var response = await mediator.Send(query, cancellationToken);

        return response;
    }
}