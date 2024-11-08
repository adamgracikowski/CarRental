namespace CarRental.Provider.API.Requests.Customers.Validators;

public class ValidatorsConstants
{
    public static class Customer
    {
        public const int FirstNameMaxLength = 50;
        public const int FirstNameMinLength = 2;

        public const int LastNameMaxLength = 50;
        public const int LastNameMinLength = 2;

        public const int EmailAddressMaxLength = 100;
    }
}