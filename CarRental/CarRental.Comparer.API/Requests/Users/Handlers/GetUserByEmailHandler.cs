using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.Users;
using CarRental.Comparer.API.Requests.Users.Queries;
using CarRental.Comparer.Persistence.Specifications.Users;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Handlers;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<CreateUserDto>>
{
    private readonly IRepositoryBase<User> usersRepository;
    private readonly IMapper mapper;

    public GetUserByEmailQueryHandler(
        IRepositoryBase<User> usersRepository,
        IMapper mapper)
    {
        this.usersRepository = usersRepository;
        this.mapper = mapper;
    }

    public async Task<Result<CreateUserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var specification = new UserByEmailSpecification(request.email);

        var user = await this.usersRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (user == null)
        {
            return Result<CreateUserDto>.NotFound();
        }

        var createUserDto = this.mapper.Map<CreateUserDto>(user);

        return Result<CreateUserDto>.Success(createUserDto);
    }
}