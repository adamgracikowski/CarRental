﻿using Ardalis.Result;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Commands;

public sealed record ReturnRentalTransactionCommand(
	int Id,
	string? Email
) : IRequest<Result>;