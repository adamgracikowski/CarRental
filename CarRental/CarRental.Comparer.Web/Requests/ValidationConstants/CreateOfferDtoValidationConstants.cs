namespace CarRental.Comparer.Web.Requests.ValidationConstants;

public static class CreateOfferDtoValidationConstants
{
	public static class AgeConstants
	{
		public const int LowerAge = 18;
		public const int UpperAge = 100;
		public const string AgeRange = "Age must be between 18 and 100.";
	}
	public static class DrivingLicenseYearsConstants
	{
		public const int LowerYears = 0;
		public const int UpperYears = 100;
		public const string DrivingLicenseYearsRange = "Driving License Years must be between 0 and 100.";
	}
}