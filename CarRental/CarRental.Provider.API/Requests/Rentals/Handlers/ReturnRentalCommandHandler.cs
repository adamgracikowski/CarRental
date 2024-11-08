using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.DTOs.Rentals;
using CarRental.Provider.API.Requests.Rentals.Commands;
using CarRental.Provider.Persistence.Specifications.Rentals;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Handlers;

public class ReturnRentalCommandHandler : IRequestHandler<ReturnRentalCommand, Result<RentalStatusDto>>
{
    private readonly IRepositoryBase<Rental> rentalsRepository;
    private readonly IMapper mapper;

    public ReturnRentalCommandHandler(
        IRepositoryBase<Rental> rentalsRepository,
        IMapper mapper)
    {
        this.rentalsRepository = rentalsRepository;
        this.mapper = mapper;
    }

    public async Task<Result<RentalStatusDto>> Handle(ReturnRentalCommand request, CancellationToken cancellationToken)
    {
        var specification = new RentalByIdSpecification(request.Id);

        var rental = await this.rentalsRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (rental == null)
        {
            return Result<RentalStatusDto>.NotFound();
        }

        if (rental.Status == RentalStatus.ReadyForReturn)
        {
            return Result<RentalStatusDto>.Success(this.mapper.Map<RentalStatusDto>(rental));
        }

        if (rental.Status != RentalStatus.Active)
        {
            return Result<RentalStatusDto>.Invalid(
                    new ValidationError(nameof(Rental.Status), $"Rental is not in the {nameof(RentalStatus.Active)} state.")
                );
        }

        rental.Status = RentalStatus.ReadyForReturn;

        await this.rentalsRepository.UpdateAsync(rental, cancellationToken);
        await this.rentalsRepository.SaveChangesAsync(cancellationToken);

        // send email here...

        var rentalStatusDto = this.mapper.Map<RentalStatusDto>(rental);

        return Result<RentalStatusDto>.Success(rentalStatusDto);
    }
}