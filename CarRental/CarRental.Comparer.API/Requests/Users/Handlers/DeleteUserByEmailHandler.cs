using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.Requests.Users.Commands;
using CarRental.Comparer.Persistence.Specifications.Users;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Handlers;

public class DeleteUserByEmailQueryHandler : IRequestHandler<DeleteUserByEmailCommand, Result>
{
    private readonly IRepositoryBase<User> usersRepository;

    public DeleteUserByEmailQueryHandler(IRepositoryBase<User> usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    public async Task<Result> Handle(DeleteUserByEmailCommand request, CancellationToken cancellationToken)
    {
        var specification = new UserByEmailSpecification(request.Email);

        var user = await this.usersRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (user == null)
        {
            return Result.NotFound();
        }

        await this.usersRepository.DeleteAsync(user, cancellationToken);

        return Result.Success();
    }
}