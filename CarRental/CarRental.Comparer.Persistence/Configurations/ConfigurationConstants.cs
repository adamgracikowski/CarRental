using CarRental.Common.Core.Enums;

namespace CarRental.Comparer.Persistence.Configurations;

public static class ConfigurationConstants
{
	public static class UserConstants
	{
		public const int EmailMaxLength = 100;

		public const int NameMaxLength = 50;

		public const int LastNameMaxLength = 50;
	}

	public static class RentalTransactionConstants
	{
		public const int RentalOuterIdMaxLength = 50;

		public const string DecimalPrecision = "decimal(18,2)";

		public static readonly RentalStatus DefaultRentalStatus = RentalStatus.Unconfirmed;

		public const int DescriptionMaxLength = 500;

		public const int ImageMaxLength = 200;
	}

	public static class ProviderConstants
	{
		public const int NameMaxLength = 100;
	}

	public static class EmployeeConstants
	{
		public const int EmailMaxLength = 100;

		public const int NameMaxLength = 50;

		public const int LastNameMaxLength = 50;
	}

	public static class CarDetailsConstants
	{
		public const int NumberOfDoorsDefaultValue = 4;

		public const int NumberOfSeatsDefaultValue = 5;

		public static readonly FuelType DefaultFuelType = FuelType.Gasoline;

		public static readonly TransmissionType DefaultTransmissionType = TransmissionType.Manual;
	}

	public static class LocalizationConstants
	{
		public const int LongitudePrecision = 9;
		public const int LongitudeScale = 6;
		public const int LongitudeMin = -180;
		public const int LongitudeMax = 180;

		public const int LatitudePrecision = 8;
		public const int LatitudeScale = 6;
		public const int LatitudeMin = -90;
		public const int LatitudeMax = 90;
	}
}