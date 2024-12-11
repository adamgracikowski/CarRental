using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Common.Core.Roles;
using CarRental.Comparer.API.Authorization;
using CarRental.Comparer.API.DTOs.RentalTransactions;
using CarRental.Comparer.API.DTOs.Reports;
using CarRental.Comparer.API.Requests.RentalTransactions.Commands;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Comparer.API.Controllers;

[Authorize]
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

	[Authorize(Policy = AuthorizationRoles.Employee)]
	[TranslateResultToActionResult]
	[HttpPost("Reports")]
	public async Task<IActionResult> GenerateReport(GenerateReportDto generateReportDto, CancellationToken cancellationToken)
	{
		var email = User.GetEmailClaim();

		var command = new GenerateReportCommand(email, generateReportDto);

		var response = await mediator.Send(command, cancellationToken);

		if (!response.IsSuccess)
		{
			return BadRequest(response.ValidationErrors.Select(e => new
			{
				e.Identifier,
				e.ErrorMessage
			}));
		}

		return File(
			response.Value.ReportContents,
			response.Value.ContentType,
			fileDownloadName: response.Value.ReportName
		);
	}

	[Authorize(Policy = AuthorizationRoles.Employee)]
	[TranslateResultToActionResult]
	[HttpGet("{status}")]
	public async Task<Result<RentalTransactionsForEmployeePaginatedDto>> GetRentalTransactionsForEmployee(string status, [FromQuery] int page,
	[FromQuery] int size, CancellationToken cancellationToken)
	{
		var email = User.GetEmailClaim();

		var query = new GetRentalTransactionsForEmployeeQuery(email, status, page, size);

		var response = await mediator.Send(query, cancellationToken);

		return response;
	}

	[Authorize(Policy = AuthorizationRoles.Employee)]
	[TranslateResultToActionResult]
	[HttpPatch("{id}/accept-return")]
	public async Task<Result> AcceptReturn(int id, [FromForm] AcceptRentalReturnDto acceptRentalReturnDto, CancellationToken cancellationToken)
	{
		var email = User.GetEmailClaim();

		var command = new AcceptRentalReturnCommand(id, email, acceptRentalReturnDto);

		var response = await mediator.Send(command, cancellationToken);

		return response;
	}
}