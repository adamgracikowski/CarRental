using FluentValidation;

namespace CarRental.Comparer.Web.Validators;

public class AbstractComponentValidator<T> : AbstractValidator<T>
{
	public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
	{
		var context = ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName));
		var result = await ValidateAsync(context);
		
		if (result.IsValid)
		{
			return [];
		}

		var errors = result.Errors
			.Select(e => e.ErrorMessage);

		return errors;
	};
}