using System.ComponentModel.DataAnnotations;
using CarRental.Comparer.Web.Extensions.DateExtensions;

namespace CarRental.Comparer.Web.Requests.DTOs.Users;

public static class UserDtoValidator
{
	public static ValidationResult ValidateBirthdate(DateTime? birthday, ValidationContext context)
	{
		if (birthday == null)
		{
			return new ValidationResult("Birthday is required");
		}

		var yearsCalculator = (IYearsCalculator?)context.GetService(typeof(IYearsCalculator));
		if (yearsCalculator == null)
		{
			throw new InvalidOperationException("YearsCalculator is not registered.");
		}

		if (18 > yearsCalculator.CalculateDifferenceInYearsFromUtcNow(birthday.Value))
		{
			return new ValidationResult("User must be at least 18 years old.");
		}

		return ValidationResult.Success;
	}

	public static ValidationResult ValidateDrivingLicenseDate(DateTime? drivingLicenseDate, ValidationContext context)
	{
		if (drivingLicenseDate == null)
		{
			return new ValidationResult("Driving license date is required");
		}

		var instance = context.ObjectInstance as UserDto;
		if (instance == null)
		{
			throw new InvalidCastException("Error while casting UserDto");
		}

		DateTime birthday = instance.Birthday.Value;
		if (drivingLicenseDate.Value < birthday.AddYears(18))
		{
			return new ValidationResult("Driving license date cannot be earlier than the 18th birthday.");
		}

		return ValidationResult.Success!;
	}
}