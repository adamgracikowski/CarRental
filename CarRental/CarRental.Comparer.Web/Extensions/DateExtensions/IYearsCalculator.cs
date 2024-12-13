namespace CarRental.Comparer.Web.Extensions.DateExtensions;

public interface IYearsCalculator
{
	public int CalculateDifferenceInYearsFromUtcNow(DateTime date);
}