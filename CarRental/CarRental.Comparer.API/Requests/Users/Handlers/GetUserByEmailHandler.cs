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
    private readonly IRepositoryBase<User> _usersRepository;
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(
        IRepositoryBase<User> rentalsRepository,
        IMapper mapper)
    {
        _usersRepository = rentalsRepository;
        _mapper = mapper;
    }

    public async Task<Result<CreateUserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var specification = new UserByEmailSpecification(request.email);

        var user = await _usersRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (user == null)
        {
            return Result<CreateUserDto>.NotFound();
        }

        var userDto = _mapper.Map<CreateUserDto>(user);

        return Result<CreateUserDto>.Success(userDto);
    }
}
