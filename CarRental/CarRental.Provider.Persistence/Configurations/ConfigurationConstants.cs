namespace CarRental.Provider.Persistence.Configurations;

public static class ConfigurationConstants
{
    public static class InsuranceConstants
    {
        public const int NameMaxLength = 50;
        public const int DescriptionMaxLength = 300;
    }

    public static class SegmentConstants
    {
        public const int NameMaxLength = 50;
        public const int DescriptionMaxLength = 300;
    }

    public static class MakeConstants
    {
        public const int NameMaxLength = 50;
    }

    public static class ModelConstants
    {
        public const int NameMaxLength = 50;
        public const int NumberOfDoorsDefaultValue = 4;
        public const int NumberOfSeatsDefaultValue = 5;
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

    public static class RentalReturnConstants
    {
        public const int DescriptionMaxLength = 300;
        public const int ImageMaxLength = 50;
    }

    public static class CustomerConstants
    {
        public const int FirstNameMaxLength = 50;
        public const int LastNameMaxLength = 50;
        public const int EmailAddressMaxLength = 100;
    }

    public static class PriceConstants
    {
        public const string DatabaseType = "decimal(18,2)";
    }
}