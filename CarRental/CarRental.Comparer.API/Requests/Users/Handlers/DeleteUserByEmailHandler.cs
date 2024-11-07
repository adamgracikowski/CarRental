using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.Requests.Users.Commands;
using CarRental.Comparer.Persistence.Specifications.Users;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Handlers;

public class DeleteUserByEmailQueryHandler : IRequestHandler<DeleteUserByEmailCommand, Result>
{
    private readonly IRepositoryBase<User> _usersRepository;
    private readonly IMapper _mapper;

    public DeleteUserByEmailQueryHandler(
        IRepositoryBase<User> rentalsRepository,
        IMapper mapper)
    {
        _usersRepository = rentalsRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(DeleteUserByEmailCommand request, CancellationToken cancellationToken)
    {
        var specification = new UserByEmailSpecification(request.email);

        var user = await _usersRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (user == null)
        {
            return Result.NotFound();
        }

        await _usersRepository.DeleteAsync(user, cancellationToken);

        return Result.Success(default);
    }
}
