using CarRental.Provider.Infrastructure.EmailService.Options;
using Microsoft.Extensions.Options;

namespace CarRental.Provider.Infrastructure.EmailService;

public sealed class EmailInputMaker : IEmailInputMaker
{
    private readonly ConfirmOfferTemplate confirmOfferTemplate;
    private readonly SendEmailOptions sendEmailOptions;

    public EmailInputMaker(IOptions<SendEmailOptions> sendEmailOptions, ConfirmOfferTemplate confirmOfferTemplate)
    {
        this.sendEmailOptions = sendEmailOptions.Value;
        this.confirmOfferTemplate = confirmOfferTemplate;
    }

    public SendEmailInput GenerateConfirmOfferInput(string ToEmail, string ToName, string OfferKey, string Make, string Model, int RentalId)
    {
        string FromEmail = sendEmailOptions.FromEmail;
        string FromName = sendEmailOptions.FromName;

        string subject = "Confirm Car Rental";

        string link = sendEmailOptions.LinkTemplate
            .Replace("{{RentalId}}", RentalId.ToString())
            .Replace("{{OfferKey}}", OfferKey);

        var Content = confirmOfferTemplate.Template
            .Replace("{{make}}", Make)
            .Replace("{{model}}", Model)
            .Replace("{{confirmation_link}}", link);

        var input = new SendEmailInput(FromEmail, FromName, ToEmail, ToName, subject, Content, true);

        return input;

    }
}