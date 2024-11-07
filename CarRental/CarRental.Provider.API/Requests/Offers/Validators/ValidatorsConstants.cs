namespace CarRental.Provider.API.Requests.Offers.Validators;

public static class ValidatorsConstants
{
    public static class CreateOffer
    {
        public const int AgeMin = 18;

        public const int LongitudeMin = -180;
        public const int LongitudeMax = 180;

        public const int LatitudeMin = -90;
        public const int LatitudeMax = 90;
    }
}