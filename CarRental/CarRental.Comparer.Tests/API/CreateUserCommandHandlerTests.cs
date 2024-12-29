using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.Users;
using CarRental.Comparer.API.Requests.Users.Commands;
using CarRental.Comparer.API.Requests.Users.Handlers;
using CarRental.Comparer.Persistence.Specifications.Users;
using CarRental.Comparer.Tests.Dummies;
using FluentAssertions;
using Moq;

namespace CarRental.Comparer.Tests.API;

public sealed class CreateUserCommandHandlerTests
{
	private readonly Mock<IRepositoryBase<User>> usersRepositoryMock;
	private readonly Mock<IMapper> mapperMock;
	private readonly CreateUserCommandHandler handler;

	private readonly string email;
	private readonly UserDto userDto;
	private readonly User user;
	private readonly CreateUserCommand command;

	public CreateUserCommandHandlerTests()
	{
		usersRepositoryMock = new();
		mapperMock = new();

		handler = new CreateUserCommandHandler(
			usersRepositoryMock.Object,
			mapperMock.Object
		);

		email = "user@example.com";
		userDto = new UserDto(
			Email: email,
			Name: string.Empty,
			Lastname: string.Empty,
			Birthday: default,
			DrivingLicenseDate: default,
			Longitude: default,
			Latitude: default
		);

		user = ComparerEntitiesDummyFactory.CreateUserDummy();
		user.Email = email;

		command = new CreateUserCommand(UserDto: userDto);
	}

	[Fact]
	public async Task Handle_WhenUserAlreadyExists_ReturnsConflict()
	{
		// Arrange
		usersRepositoryMock
			.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UserByEmailSpecification>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(user);

		// Act
		var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		usersRepositoryMock.Verify(r => r.FirstOrDefaultAsync(
			It.IsAny<UserByEmailSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once
		);

		usersRepositoryMock.VerifyNoOtherCalls();

		result.Status.Should().Be(ResultStatus.Conflict);
	}

	[Fact]
	public async Task Handle_WhenUserDoesNotExist_CreatesAndReturnsUser()
	{
		// Arrange
		var userIdDto = new UserIdDto(Id: default);

		mapperMock.Setup(m => m.Map<User>(userDto))
			.Returns(user);

		usersRepositoryMock.Setup(r => r.AddAsync(user, It.IsAny<CancellationToken>()))
			.ReturnsAsync(user);

		mapperMock.Setup(m => m.Map<UserIdDto>(user))
			.Returns(userIdDto);

		// Act
		var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		usersRepositoryMock.Verify(r => r.FirstOrDefaultAsync(
			It.IsAny<UserByEmailSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once
		);

		usersRepositoryMock.Verify(r => r.AddAsync(user, It.IsAny<CancellationToken>()), Times.Once);
		usersRepositoryMock.VerifyNoOtherCalls();

		result.Status.Should().Be(ResultStatus.Created);
		result.Value.Should().Be(userIdDto);
	}
}