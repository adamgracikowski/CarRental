namespace CarRental.Provider.Infrastructure.EmailService;

public interface IEmailInputMaker
{
    public SendEmailInput GenerateConfirmOfferInput(string ToEmail, string ToName, string OfferKey, string Make, string Model, int RentalId);
}
