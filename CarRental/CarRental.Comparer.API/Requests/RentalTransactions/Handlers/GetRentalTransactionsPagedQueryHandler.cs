using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;
using FluentValidation;
using CarRental.Comparer.API.DTOs.RentalTransactions;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using CarRental.Comparer.Persistence.Specifications.Users;
using CarRental.Comparer.API.Pagination;
using MediatR;
using Ardalis.Result.FluentValidation;
using CarRental.Comparer.Infrastructure.CarComparisons;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Handlers;

public class
	GetRentalTransactionsPagedQueryHandler : IRequestHandler<GetRentalTransactionsPagedQuery,
	Result<RentalTransactionPaginatedListDto>>
{
	private readonly IRepositoryBase<User> userRepository;
	private readonly IMapper mapper;
	private readonly IValidator<GetRentalTransactionsPagedQuery> validator;
	private readonly ICarComparisonService carComparisonService;

	public GetRentalTransactionsPagedQueryHandler(
		IRepositoryBase<User> userRepository,
		IMapper mapper,
		IValidator<GetRentalTransactionsPagedQuery> validator,
		ICarComparisonService carComparisonService)
	{
		this.userRepository = userRepository;
		this.mapper = mapper;
		this.validator = validator;
		this.carComparisonService = carComparisonService;
	}

	public async Task<Result<RentalTransactionPaginatedListDto>> Handle(GetRentalTransactionsPagedQuery request,
		CancellationToken cancellationToken)
	{
		var validation = await validator.ValidateAsync(request, cancellationToken);

		if (!validation.IsValid)
		{
			return Result<RentalTransactionPaginatedListDto>.Invalid(validation.AsErrors());
		}

		var rentalStatus = Enum.Parse<RentalStatus>(request.Status, true);

		var specification = new UserByEmailWithRentalsByStatusWithCarProviderSpecification(request.Email, rentalStatus);

		var user = await userRepository.FirstOrDefaultAsync(specification);

		if (user == null)
		{
			return Result<RentalTransactionPaginatedListDto>.Invalid(new ValidationError(request.Email, "Email address was not found"));
		}

		var rentals = user.RentalTransactions;


		if (rentalStatus == RentalStatus.ReadyForReturn)
		{
			await CheckForReturnedCarsAsync(rentals, cancellationToken);
			rentals = rentals.Where(r => r.Status == RentalStatus.ReadyForReturn).ToList();
		}

		if (rentalStatus == RentalStatus.Returned)
		{
			var readyForReturnSpecification = new UserByEmailWithRentalsByStatusWithCarProviderSpecification(request.Email, RentalStatus.ReadyForReturn);

			user = await userRepository.FirstOrDefaultAsync(readyForReturnSpecification);

			if (user == null)
			{
				return Result<RentalTransactionPaginatedListDto>.Invalid(new ValidationError(request.Email, "Email address was not found"));
			}

			await CheckForReturnedCarsAsync(user.RentalTransactions, cancellationToken);

			rentals = rentals.Where(r => r.Status == RentalStatus.Returned).ToList();
		}

		var rentalList = rentals.ToList();

		rentalList.Sort((r1, r2) => r2.RentedAt.CompareTo(r1.RentedAt));

		var rentalDtos = mapper.Map<List<RentalTransactionDto>>(rentalList);

		int numberOfPages = rentalDtos.NumberOfPages(request.PageSize);

		var page = rentalDtos.GetPage(request.PageSize, request.PageNumber);

		var paginationInfo = new PaginationInfo(request.PageNumber, request.PageSize, numberOfPages);

		var rentalDtoList = new RentalTransactionPaginatedListDto(page, paginationInfo);

		return rentalDtoList;
	}

	private async Task CheckForReturnedCarsAsync(ICollection<RentalTransaction> rentals, CancellationToken cancellationToken)
	{
		foreach (var transaction in rentals)
		{
			if (transaction.Status == RentalStatus.Returned)
				continue;

			var statusName = await carComparisonService.GetRentalStatusByIdAsync(transaction.Provider.Name, transaction.RentalOuterId, cancellationToken);

			if (statusName == null)
				continue;

			if (!Enum.TryParse<RentalStatus>(statusName.status, true, out RentalStatus status))
				continue;

			transaction.Status = status;
		}

		await userRepository.SaveChangesAsync(cancellationToken);
	}
}