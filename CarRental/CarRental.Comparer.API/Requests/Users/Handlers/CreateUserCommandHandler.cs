using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.Users;
using CarRental.Comparer.API.Requests.Users.Commands;
using CarRental.Comparer.Persistence.Specifications.Users;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserIdDto>>
{
    private readonly IRepositoryBase<User> usersRepository;
    private readonly IMapper mapper;

    public CreateUserCommandHandler(
        IRepositoryBase<User> usersRepository,
        IMapper mapper)
    {
        this.usersRepository = usersRepository;
        this.mapper = mapper;
    }

    public async Task<Result<UserIdDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var specification = new UserByEmailSpecification(request.UserDto.Email);

        var userInDatabase = await this.usersRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (userInDatabase != null)
        {
            return Result<UserIdDto>.Conflict();
        }

        var user = this.mapper.Map<User>(request.UserDto);

        await this.usersRepository.AddAsync(user, cancellationToken);

        var userIdDto = this.mapper.Map<UserIdDto>(user);

        return Result<UserIdDto>.Created(userIdDto);
    }
}