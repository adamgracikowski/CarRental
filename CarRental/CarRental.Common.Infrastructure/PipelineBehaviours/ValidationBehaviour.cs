using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;

namespace CarRental.Common.Infrastructure.PipelineBehaviours;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> validators;

	public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
	{
		this.validators = validators;
	}

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		if (validators.Any())
		{
			var validationContext = new ValidationContext<TRequest>(request);

			var validationResults = await Task.WhenAll(
				validators.Select(v => v.ValidateAsync(validationContext, cancellationToken)
			));

			var validationErrors = validationResults
				.SelectMany(r => r.AsErrors())
				.ToList();

			var validationFailures = validationResults
				.SelectMany(r => r.Errors)
				.Where(f => f != null)
				.ToList();

			if (validationFailures.Count != 0)
			{
				var responseType = typeof(TResponse);
				if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<>))
				{
					var resultType = responseType.GetGenericArguments()[0];

					var invalidMethod = typeof(Result<>)
						.MakeGenericType(resultType)
						.GetMethod(nameof(Result<int>.Invalid), [typeof(List<ValidationError>)]);

					if (invalidMethod != null)
					{
						return (TResponse)invalidMethod.Invoke(null, [validationErrors])!;
					}
				}
				else if (typeof(TResponse) == typeof(Result))
				{
					return (TResponse)(object)Result.Invalid(validationErrors);
				}
				else
				{
					throw new ValidationException(validationFailures);
				}
			}
		}

		return await next();
	}
}