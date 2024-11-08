using Ardalis.Result;
using CarRental.Provider.API.DTOs.RentalReturns;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Commands;

public sealed record AcceptRentalReturnCommand(int Id, AcceptRentalReturnDto AcceptRentalReturnDto) 
    : IRequest<Result<RentalReturnDto>>;