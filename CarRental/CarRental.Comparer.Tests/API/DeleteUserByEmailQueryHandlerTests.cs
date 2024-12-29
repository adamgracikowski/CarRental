using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.Requests.Users.Commands;
using CarRental.Comparer.API.Requests.Users.Handlers;
using CarRental.Comparer.Persistence.Specifications.Users;
using CarRental.Comparer.Tests.Dummies;
using FluentAssertions;
using Moq;

namespace CarRental.Comparer.Tests.API;
public sealed class DeleteUserByEmailQueryHandlerTests
{
	private readonly Mock<IRepositoryBase<User>> usersRepositoryMock;
	private readonly DeleteUserByEmailQueryHandler handler;
	private readonly DeleteUserByEmailCommand command;

	public DeleteUserByEmailQueryHandlerTests()
	{
		usersRepositoryMock = new();

		handler = new DeleteUserByEmailQueryHandler(
			usersRepositoryMock.Object
		);

		command = new DeleteUserByEmailCommand(Email: string.Empty);
	}

	[Fact]
	public async Task Handle_WhenUserDoesNotExist_ReturnsNotFound()
	{
		// Act
		var response = await handler.Handle(command, CancellationToken.None);

		// Assert
		usersRepositoryMock.Verify(r => r.FirstOrDefaultAsync(
			It.IsAny<UserByEmailSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once
		);

		usersRepositoryMock.VerifyNoOtherCalls();

		response.Status.Should().Be(ResultStatus.NotFound);
	}

	[Fact]
	public async Task Handle_WhenUserExists_DeletesUserAndReturnsSuccess()
	{
		// Arrange
		var user = ComparerEntitiesDummyFactory.CreateUserDummy();

		usersRepositoryMock.Setup(r => r.FirstOrDefaultAsync(
			It.IsAny<UserByEmailSpecification>(),
			It.IsAny<CancellationToken>())
		).ReturnsAsync(user);

		// Act
		var response = await handler.Handle(command, CancellationToken.None);

		// Assert
		usersRepositoryMock.Verify(r => r.FirstOrDefaultAsync(
			It.IsAny<UserByEmailSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once
		);

		usersRepositoryMock.Verify(r => r.DeleteAsync(
			user,
			It.IsAny<CancellationToken>()),
			Times.Once
		);

		usersRepositoryMock.VerifyNoOtherCalls();

		response.Status.Should().Be(ResultStatus.Ok);
	}
}