namespace CarRental.Comparer.API.Validators;

public static class ValidatorsConstants
{
	public static class ChooseOfferDtoConstants
	{
		public const int EmailAddressMaxLength = 100;
	}

	public static class AcceptRentalReturnDtoConstants
	{
		public const int DescriptionMaxLength = 500;
		public const int MaxLatitude = 90;
		public const int MinLatitude = -90;
		public const int MaxLongitude = 180;
		public const int MinLongitude = -180;
	}
}