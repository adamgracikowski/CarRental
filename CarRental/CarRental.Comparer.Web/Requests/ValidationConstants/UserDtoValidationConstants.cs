namespace CarRental.Comparer.Web.Requests.ValidationConstants;

public static class UserDtoValidationConstants
{
    public static class EmailConstants
    {
        public const int MaxLength = 100;
        public const string EmailIsRequired = "Email is required.";
        public const string EmailErrorMessage = "Invalid Email Address.";
        public const string EmailMaxLength = $"Email cannot exceed 100 characters.";
    }

    public static class NameConstants
    {
        public const int MaxLength = 50;
        public const string FirstNameIsRequired = "First Name is required.";
        public const string FirstNameMaxLength = "First Name cannot exceed 50 characters.";
    }

    public static class LastNameConstants
    {
        public const int MaxLength = 50;
        public const string LastNameIsRequired = "Last Name is required.";
        public const string LastNameMaxLength = "Last Name cannot exceed 50 characters.";
    }

    public static class AgeConstants
    {
        public const int LowerAge = 18;
        public const int UpperAge = 100;
        public const string AgeRange = "Age must be between 18 and 100.";
    }

    public static class DrivingLicenseYearsConstants
    {
        public const int LowerYears = 1;
        public const int UpperYears = 100;
        public const string DrivingLicenseYearsRange = "Driving License Years must be between 1 and 100.";
    }

    public static class PositionConstants
    {
        public const int LongtitudeMax = 180;
        public const int LongtitudeMin = -180;

        public const int LatitudeMax = 90;
        public const int LatitudeMin = -90;

        public const string PositionIsRequired = "Position is required.";
        public const string InvalidLongitude = "Longitude must be between -180 and 180.";
        public const string InvalidLatitude = "Latitude must be between -90 and 90.";
    }
}