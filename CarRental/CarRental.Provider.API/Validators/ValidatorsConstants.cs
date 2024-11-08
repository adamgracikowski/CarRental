namespace CarRental.Provider.API.Validators;

public static class ValidatorsConstants
{
    public static class CreateOfferConstants
    {
        public const int AgeMin = 18;
    }

    public static class LocalizationConstants
    {
        public const int LongitudeMin = -180;
        public const int LongitudeMax = 180;

        public const int LatitudeMin = -90;
        public const int LatitudeMax = 90;
    }

    public static class AcceptRentalReturnConstants
    {
        public const int DescriptionMaxLength = 300;
        public const int ImageMaxSize = 5 * 1024 * 1024; // 5 MB

        public static readonly string[] ImageAllowedExtensions = [".jpg", ".jpeg", ".png"];
        public static readonly string[] ImageAllowedMimeTypes = ["image/jpeg", "image/png"];

    }

    public static class CustomerConstants
    {
        public const int FirstNameMaxLength = 50;
        public const int FirstNameMinLength = 2;

        public const int LastNameMaxLength = 50;
        public const int LastNameMinLength = 2;

        public const int EmailAddressMaxLength = 100;
    }
}