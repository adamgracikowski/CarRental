﻿using Ardalis.Result;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;
using MediatR;

namespace CarRental.Comparer.API.Requests.Providers.Commands;

public sealed record ChooseOfferCommand(int id, int offerId, ChooseOfferDto chooseOfferDto) : IRequest<Result<RentalTransactionIdDto>>;
