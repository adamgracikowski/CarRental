using Ardalis.Result;
using CarRental.Comparer.API.DTOs.Reports;
using CarRental.Comparer.Infrastructure.Reports;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Commands;

public sealed record GenerateReportCommand(string? Email, GenerateReportDto GenerateReportDto) : IRequest<Result<ReportResult>>;