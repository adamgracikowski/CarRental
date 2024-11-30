namespace CarRental.Comparer.Web.DateExtensions;

public interface IYearsCalculator
{
	public int CalculateDifferenceInYearsFromUtcNow(DateTime date);
}