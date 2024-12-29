using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.Users;
using CarRental.Comparer.API.Requests.Users.Handlers;
using CarRental.Comparer.API.Requests.Users.Queries;
using CarRental.Comparer.Persistence.Specifications.Users;
using CarRental.Comparer.Tests.Dummies;
using FluentAssertions;
using Moq;

namespace CarRental.Comparer.Tests.API;
public sealed class GetUserByEmailQueryHandlerTests
{
	private readonly Mock<IRepositoryBase<User>> usersRepositoryMock;
	private readonly Mock<IMapper> mapperMock;
	private readonly GetUserByEmailQueryHandler handler;

	public GetUserByEmailQueryHandlerTests()
	{
		usersRepositoryMock = new();
		mapperMock = new();

		handler = new GetUserByEmailQueryHandler(
			usersRepositoryMock.Object,
			mapperMock.Object
		);
	}

	[Fact]
	public async Task Handle_WhenUserDoesNotExist_ReturnsNotFound()
	{
		// Arrange
		var query = new GetUserByEmailQuery(Email: string.Empty);

		// Act
		var response = await handler.Handle(query, CancellationToken.None);

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
	public async Task Handle_WhenUserExists_ReturnsUser()
	{
		// Arrange
		var query = new GetUserByEmailQuery(Email: string.Empty);
		var user = ComparerEntitiesDummyFactory.CreateUserDummy();

		var userDto = new UserDto(
			Email: string.Empty,
			Name: string.Empty,
			Lastname: string.Empty,
			Birthday: default,
			DrivingLicenseDate: default,
			Longitude: default,
			Latitude: default
		);

		usersRepositoryMock.Setup(r => r.FirstOrDefaultAsync(
			It.IsAny<UserByEmailSpecification>(),
			It.IsAny<CancellationToken>())
		).ReturnsAsync(user);

		mapperMock.Setup(m => m.Map<UserDto>(user))
			.Returns(userDto);

		// Act
		var response = await handler.Handle(query, CancellationToken.None);

		// Assert
		usersRepositoryMock.Verify(r => r.FirstOrDefaultAsync(
			It.IsAny<UserByEmailSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once
		);

		usersRepositoryMock.VerifyNoOtherCalls();

		response.Status.Should().Be(ResultStatus.Ok);
		response.Value.Should().BeEquivalentTo(userDto);
	}
}