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

	/// <summary>
	/// Retrieves the status of a rental transaction by its ID.
	/// </summary>
	/// <param name="id">The ID of the rental transaction.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The status of the rental transaction.</returns>
	/// <response code="200">The rental transaction status was retrieved successfully.</response>
	/// <response code="404">The specified rental transaction was not found.</response>
	/// <response code="500">An internal server error occurred while retrieving the status.</response>
	[TranslateResultToActionResult]
	[HttpGet("{id}/status")]
	public async Task<Result<RentalTransactionStatusDto>> GetRentalTransactionStatus(int id, CancellationToken cancellationToken)
	{
		var query = new GetRentalTransactionStatusByIdQuery(id);

		var response = await mediator.Send(query, cancellationToken);

		return response;
	}

	/// <summary>
	/// Generates a report for rental transactions for a given period of time.
	/// </summary>
	/// <param name="generateReportDto">The data required to generate the report.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The generated report as a downloadable file.</returns>
	/// <response code="200">The report was generated successfully.</response>
	/// <response code="400">The request data was invalid.</response>
	/// <response code="500">An internal server error occurred while generating the report.</response>
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

	/// <summary>
	/// Retrieves a paginated list of rental transactions for employees based on their status.
	/// </summary>
	/// <param name="status">The status of the rental transactions to filter by.</param>
	/// <param name="page">The page number for pagination.</param>
	/// <param name="size">The number of items per page for pagination.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A paginated list of rental transactions for employees.</returns>
	/// <response code="200">The list of rental transactions was retrieved successfully.</response>
	/// <response code="400">The pagination parameters were invalid.</response>
	/// <response code="403">The user is not authorized to view the rental transactions.</response>
	/// <response code="500">An internal server error occurred while retrieving the transactions.</response>
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

	/// <summary>
	/// Accepts the return of a rental transaction.
	/// </summary>
	/// <param name="id">The ID of the rental transaction to accept the return for.</param>
	/// <param name="acceptRentalReturnDto">The data required to process the return.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>An empty result indicating success or failure.</returns>
	/// <response code="200">The return was successfully accepted.</response>
	/// <response code="400">The request data was invalid.</response>
	/// <response code="403">The user is not authorized to accept this return.</response>
	/// <response code="404">The specified rental transaction was not found.</response>
	/// <response code="500">An internal server error occurred while accepting the return.</response>
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

	[Authorize(Policy = AuthorizationRoles.User)]
	[TranslateResultToActionResult]
	[HttpPatch("{id}/return")]
	public async Task<Result> ReturnRentalTransaction(int id, CancellationToken cancellationToken)
	{
		var email = User.GetEmailClaim();

		var command = new ReturnRentalTransactionCommand(id, email);

		var response = await mediator.Send(command, cancellationToken);

		return response;
	}
}