using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.Requests.Rentals.DTOs;
using CarRental.Provider.API.Requests.Rentals.Queries;
using CarRental.Provider.Persistence.Specifications.Rentals;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Handlers;

public class GetRentalByIdQueryHandler : IRequestHandler<GetRentalByIdQuery, Result<RentalStatusDto>>
{
    private readonly IRepositoryBase<Rental> rentalsRepository;
    private readonly IMapper mapper;

    public GetRentalByIdQueryHandler(
        IRepositoryBase<Rental> rentalsRepository,
        IMapper mapper)
    {
        this.rentalsRepository = rentalsRepository;
        this.mapper = mapper;
    }

    public async Task<Result<RentalStatusDto>> Handle(GetRentalByIdQuery request, CancellationToken cancellationToken)
    {
        var specification = new RentalByIdSpecification(request.Id);

        var rental = await this.rentalsRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (rental == null)
        {
            return Result<RentalStatusDto>.NotFound();
        }

        var rentalStatusDto = this.mapper.Map<RentalStatusDto>(rental);

        return Result<RentalStatusDto>.Success(rentalStatusDto);
    }
}