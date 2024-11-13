using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.Users;
using CarRental.Comparer.API.Requests.Users.Commands;
using CarRental.Comparer.Persistence.Specifications.Users;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IRepositoryBase<User> _usersRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(IRepositoryBase<User> usersRepository, IMapper mapper, ILogger<CreateUserCommandHandler> logger)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.createUserDto);

        var specification = new UserByEmailSpecification(user.Email);

        var userInDatabase = await _usersRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (userInDatabase != null)
        {
            return Result<UserDto>.Conflict();
        }

        await _usersRepository.AddAsync(user, cancellationToken);

        var createdUserDto = _mapper.Map<UserDto>(user);

        return Result<UserDto>.Created(createdUserDto);
    }
}
