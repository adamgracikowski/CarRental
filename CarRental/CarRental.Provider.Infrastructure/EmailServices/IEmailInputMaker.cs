namespace CarRental.Provider.Infrastructure.EmailServices;

public interface IEmailInputMaker
{
	SendEmailInput GenerateOfferConfirmedInput(string ToEmail, string ToName, string OfferKey, string Make, string Model, int RentalId);

	SendEmailInput GenerateRentalConfirmedInput(string ToEmail, string ToName, string Make, string Model);

	SendEmailInput GenerateRentalReturnedTemplate(string toEmail, string toName, string make, string model, DateTime rentedAt, DateTime returnedAt, decimal insurancePricePerDay, decimal rentalPricePerDay);

	SendEmailInput GenerateRentalReturnStartedTemplate(string toEmail, string toName, string make, string model);
}