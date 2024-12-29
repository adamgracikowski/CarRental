using Ardalis.Result;
using CarRental.Common.Infrastructure.PipelineBehaviours;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Moq;

namespace CarRental.Provider.Tests.Infrastructure;

public sealed class ValidationBehaviourTests
{
	private class TestResultRequest(string x)
		: IRequest<Result<string>>
	{
		public string X { get; set; } = x;
	}

	private class TestResultRequestValidator
		: AbstractValidator<TestResultRequest>
	{
		public TestResultRequestValidator()
		{
			RuleFor(t => t.X)
				.NotNull()
				.WithMessage("Validation message.");
		}
	}

	private class TestNonResultRequest(string x) 
		: IRequest<string>
	{
		public string X { get; set; } = x;
	}

	private class TestNonResultRequestValidator
	: AbstractValidator<TestNonResultRequest>
	{
		public TestNonResultRequestValidator()
		{
			RuleFor(t => t.X)
				.NotNull()
				.WithMessage("Validation message.");
		}
	}

	[Fact]
	public async Task Handle_WhenNoValidatorsAreRegistered_ShouldCallNext()
	{
		// Arrange
		var validators = Enumerable.Empty<IValidator<TestResultRequest>>();
		var behavior = new ValidationBehavior<TestResultRequest, Result<string>>(validators);
		var nextMock = new Mock<RequestHandlerDelegate<Result<string>>>();

		nextMock.Setup(x => x.Invoke())
			.ReturnsAsync(Result<string>.Success("Success"));

		// Act
		var result = await behavior.Handle(
			new TestResultRequest(x: string.Empty), 
			nextMock.Object, 
			CancellationToken.None
		);

		// Assert
		nextMock.Verify(x => x.Invoke(), Times.Once);

		result.Status.Should().Be(ResultStatus.Ok);
		result.Value.Should().Be("Success");
	}

	[Fact]
	public async Task Handle_WhenRequestIsValid_ShouldCallNext()
	{
		// Arrange
		var validators = new List<IValidator<TestResultRequest>> { new TestResultRequestValidator() };
		var behavior = new ValidationBehavior<TestResultRequest, Result<string>>(validators);
		var nextMock = new Mock<RequestHandlerDelegate<Result<string>>>();

		nextMock.Setup(x => x.Invoke())
			.ReturnsAsync(Result<string>.Success("Success"));

		// Act
		var result = await behavior.Handle(
			new TestResultRequest(x: string.Empty), 
			nextMock.Object, 
			CancellationToken.None
		);

		// Assert
		nextMock.Verify(x => x.Invoke(), Times.Once);

		result.Status.Should().Be(ResultStatus.Ok);
		result.Value.Should().Be("Success");
	}

	[Fact]
	public async Task Handle_WhenRequestIsInvalid_ShouldReturnValidationErrors()
	{
		// Arrange
		var validators = new List<IValidator<TestResultRequest>> { new TestResultRequestValidator() };
		var behavior = new ValidationBehavior<TestResultRequest, Result<string>>(validators);

		// Act
		var result = await behavior.Handle(
			new TestResultRequest(x: null!), 
			() => Task.FromResult(Result<string>.Success("Success")), 
			CancellationToken.None
		);

		// Assert
		result.Status.Should().Be(ResultStatus.Invalid);
		result.ValidationErrors.Should().HaveCount(1);

		var validationError = result.ValidationErrors.First();

		validationError.Identifier.Should().Be(nameof(TestResultRequest.X));
		validationError.ErrorMessage.Should().Be("Validation message.");
	}

	[Fact]
	public async Task Handle_WhenResponseTypeIsNonResponse_ShouldThrowValidationException()
	{
		// Arrange
		var validators = new List<IValidator<TestNonResultRequest>> { new TestNonResultRequestValidator() };
		var behavior = new ValidationBehavior<TestNonResultRequest, string>(validators);
		
		var action = async () => await behavior.Handle(
				new TestNonResultRequest(x: null!), 
				() => Task.FromResult("Success"), 
				CancellationToken.None
			);

		// Act & Assert
		await action.Should().ThrowAsync<ValidationException>()
			.WithMessage("*Validation failed*")
			.Where(e => e.Errors.Any(e => e.PropertyName == nameof(TestNonResultRequest.X)));
	}
}