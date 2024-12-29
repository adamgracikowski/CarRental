using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;
using CarRental.Comparer.API.DTOs.RentalTransactions;
using CarRental.Comparer.API.Pagination;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Infrastructure.CarProviders.RentalStatusConversions;
using CarRental.Comparer.Persistence.Specifications.Users;
using FluentValidation;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Handlers;

public class
	GetRentalTransactionsByStatusQueryHandler : IRequestHandler<GetRentalTransactionsByStatusQuery,
	Result<RentalTransactionPaginatedListDto>>
{
	private readonly IRepositoryBase<User> usersRepository;
	private readonly IMapper mapper;
	private readonly ICarComparisonService carComparisonService;
	private readonly IRentalStatusConverter rentalStatusConverter;
	private readonly ILogger<GetRentalTransactionsByStatusQueryHandler> logger;

	public GetRentalTransactionsByStatusQueryHandler(
		IRepositoryBase<User> userRepository,
		IMapper mapper,
		ICarComparisonService carComparisonService,
		IRentalStatusConverter rentalStatusConverter,
		ILogger<GetRentalTransactionsByStatusQueryHandler> logger)
	{
		this.usersRepository = userRepository;
		this.mapper = mapper;
		this.carComparisonService = carComparisonService;
		this.rentalStatusConverter = rentalStatusConverter;
		this.logger = logger;
	}

	public async Task<Result<RentalTransactionPaginatedListDto>> Handle(GetRentalTransactionsByStatusQuery request,
		CancellationToken cancellationToken)
	{
		var rentalStatus = Enum.Parse<RentalStatus>(request.Status, true);

		var specification = new UserByEmailWithRentalsByStatusWithCarProviderSpecification(request.Email, rentalStatus);

		var user = await this.usersRepository.FirstOrDefaultAsync(specification, cancellationToken);

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

			user = await this.usersRepository.FirstOrDefaultAsync(readyForReturnSpecification, cancellationToken);

			if (user == null)
			{
				return Result<RentalTransactionPaginatedListDto>.Invalid(new ValidationError(request.Email, "Email address was not found"));
			}

			await CheckForReturnedCarsAsync(user.RentalTransactions, cancellationToken);

			rentals = rentals.Where(r => r.Status == RentalStatus.Returned).ToList();
		}

		var rentalList = rentals.ToList();

		rentalList.Sort((r1, r2) => r2.RentedAt.CompareTo(r1.RentedAt));

		var rentalDtos = this.mapper.Map<List<RentalTransactionDto>>(rentalList);

		var numberOfPages = rentalDtos.NumberOfPages(request.PageSize);

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

			var rentalStatusDto = await this.carComparisonService
				.GetRentalStatusByIdAsync(transaction.Provider.Name, transaction.RentalOuterId, cancellationToken);

			if (rentalStatusDto == null)
				continue;

			if(!this.rentalStatusConverter.TryConvertFromProviderRentalStatus(rentalStatusDto.Status, transaction.Provider.Name, out var updatedStatus))
			{
				this.logger.LogWarning($"Rental status conversion for {rentalStatusDto.Status} has failed.");
				continue;
			}

			transaction.Status = updatedStatus;
		}

		await this.usersRepository.SaveChangesAsync(cancellationToken);
	}
}