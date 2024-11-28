using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CarRental.Comparer.API.DTOs.RentalTransactions;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;

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
	public async Task<Result<RentalTransactionStatusDto>> GetRentalTransactionStatus(int id, CancellationToken cancellationToken)
	{
		var query = new GetRentalTransactionStatusByIdQuery(id);

		var response = await mediator.Send(query, cancellationToken);

		return response;
	}


	[TranslateResultToActionResult]
	[HttpGet("{userEmail}/{status}")]
	public async Task<Result<RentalTransactionPaginatedListDto>> GetRentalTransactionsPaged(string userEmail, string status, [FromQuery] int page,
		[FromQuery] int size, CancellationToken cancellationToken)
	{
		var query = new GetRentalTransactionsPagedQuery(userEmail, status, page, size);

		var response = await mediator.Send(query, cancellationToken);

		return response;
	}
}