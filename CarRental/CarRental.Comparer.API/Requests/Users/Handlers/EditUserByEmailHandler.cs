using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.Users;
using CarRental.Comparer.API.Requests.Users.Commands;
using CarRental.Comparer.Persistence.Specifications.Users;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Handlers;

public class EditUserByEmailCommandHandler : IRequestHandler<EditUserByEmailCommand, Result>
{
    private readonly IRepositoryBase<User> usersRepository;
    private readonly IMapper mapper;
    private readonly ILogger<EditUserByEmailCommandHandler> logger;

    public EditUserByEmailCommandHandler(IRepositoryBase<User> usersRepository, IMapper mapper, ILogger<EditUserByEmailCommandHandler> logger)
    {
        this.usersRepository = usersRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<Result> Handle(EditUserByEmailCommand request, CancellationToken cancellationToken)
    {
        var specification = new UserByEmailSpecification(request.Email);

        var userInDatabase = await this.usersRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (userInDatabase == null)
        {
            logger.LogWarning("User with email {Email} not found", request.Email);
            return Result.NotFound();
        }

        mapper.Map(request.EditUserDto, userInDatabase);

        await this.usersRepository.UpdateAsync(userInDatabase, cancellationToken);
        await this.usersRepository.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}