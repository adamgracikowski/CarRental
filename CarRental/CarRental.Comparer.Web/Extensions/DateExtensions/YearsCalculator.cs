namespace CarRental.Comparer.Web.Extensions.DateExtensions;

public class YearsCalculator : IYearsCalculator
{
	private readonly IDateTimeProvider dateTimeProvider;

	public YearsCalculator(IDateTimeProvider dateTimeProvider)
	{
		this.dateTimeProvider = dateTimeProvider;
	}

	public int CalculateDifferenceInYearsFromUtcNow(DateTime date)
	{
		var today = dateTimeProvider.UtcNow;
		var years = today.Year - date.Year;
		return today < date.AddYears(years) ? years - 1 : years;
	}
}