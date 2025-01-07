using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.DTOs.Rentals;
using CarRental.Provider.API.Requests.Rentals.Queries;
using CarRental.Provider.Persistence.Specifications.Customers;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Handlers;

public sealed class GetCustomerRentalsQueryHandler : IRequestHandler<GetCustomerRentalsQuery, Result<CustomerRentalsDto>>
{
	private readonly IRepositoryBase<Customer> customersRepository;
	private readonly IMapper mapper;

	public GetCustomerRentalsQueryHandler(
		IRepositoryBase<Customer> customersRepository,
		IMapper mapper)
	{
		this.customersRepository = customersRepository;
		this.mapper = mapper;
	}

	public async Task<Result<CustomerRentalsDto>> Handle(GetCustomerRentalsQuery request, CancellationToken cancellationToken)
	{
		var specification = new CustomerByEmailAddressAudienceWithRentalsOffersCarsSpecification(request.EmailAddress, request.Audience);
		
		var customer = await this.customersRepository.FirstOrDefaultAsync(specification, cancellationToken);

		if (customer == null)
		{
			return Result<CustomerRentalsDto>.NotFound();
		}

		var customerRentals = this.mapper.Map<IEnumerable<CustomerRentalDto>>(customer.Rentals);
		var customerRentalsDto = new CustomerRentalsDto(customerRentals);

		return Result.Success(customerRentalsDto);
	}
}