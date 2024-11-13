using CarRental.Provider.Infrastructure.Calculators.RentalBillCalculator;
using CarRental.Provider.Infrastructure.EmailService.Options;
using CarRental.Provider.Infrastructure.EmailService.TemplateProviders;
using Microsoft.Extensions.Options;

namespace CarRental.Provider.Infrastructure.EmailService;

public sealed class EmailInputMaker : IEmailInputMaker
{
    private readonly OfferConfirmedTemplate offerConfirmedTemplate;
    private readonly SendEmailOptions sendEmailOptions;
    private readonly RentalConfirmedTemplate rentalConfirmedTemplate;
    private readonly RentalReturnConfirmedTemplate rentalReturnConfirmedTemplate;
    private readonly RentalReturnStartedTemplate rentalReturnStartedTemplate;
    private readonly IRentalBillCalculatorService rentalBillCalculatorService;

    public EmailInputMaker(IOptions<SendEmailOptions> sendEmailOptions,
        OfferConfirmedTemplate offerConfirmedTemplate, 
        RentalConfirmedTemplate rentalConfirmedTemplate, 
        RentalReturnConfirmedTemplate rentalReturnConfirmedTemplate,
        IRentalBillCalculatorService rentalBillCalculatorService,
        RentalReturnStartedTemplate rentalReturnStartedTemplate)
    {
        this.sendEmailOptions = sendEmailOptions.Value;
        this.offerConfirmedTemplate = offerConfirmedTemplate;
        this.rentalConfirmedTemplate = rentalConfirmedTemplate;
        this.rentalReturnConfirmedTemplate = rentalReturnConfirmedTemplate;
        this.rentalBillCalculatorService = rentalBillCalculatorService;
        this.rentalReturnStartedTemplate = rentalReturnStartedTemplate;
    }

    public SendEmailInput GenerateOfferConfirmedInput(
        string toEmail, 
        string toName, 
        string offerKey, 
        string make, 
        string model, 
        int rentalId)
    {
        string fromEmail = sendEmailOptions.FromEmail;
        
        string fromName = sendEmailOptions.FromName;

        string subject = "Confirm your car rental";

        string link = sendEmailOptions.LinkTemplate
            .Replace("{{RentalId}}", rentalId.ToString())
            .Replace("{{OfferKey}}", offerKey);

        var content = offerConfirmedTemplate.Template
            .Replace("{{make}}", make)
            .Replace("{{model}}", model)
            .Replace("{{name}}", toName)
            .Replace("{{confirmation_link}}", link);

        var input = new SendEmailInput(fromEmail, fromName, toEmail, toName, subject, content, true);

        return input;

    }

    public SendEmailInput GenerateRentalConfirmedInput(
        string toEmail, 
        string toName, 
        string make, 
        string model)
    {
        string fromEmail = sendEmailOptions.FromEmail;

        string fromName = sendEmailOptions.FromName;

        string subject = "Your car rental is confirmed";

        var content = rentalConfirmedTemplate.Template
            .Replace("{{make}}", make)
            .Replace("{{model}}", model)
            .Replace("{{name}}", toName);

        var input = new SendEmailInput(fromEmail, fromName, toEmail, toName, subject, content, true);

        return input;

    }

    public SendEmailInput GenerateRentalReturnedTemplate(
        string toEmail, 
        string toName, 
        string make, 
        string model, 
        DateTime rentedAt,
        DateTime returnedAt,
        decimal insurancePricePerDay,
        decimal rentalPricePerDay)
    {
        string fromEmail = sendEmailOptions.FromEmail;
        
        string fromName = sendEmailOptions.FromName;

        string subject = "Your rental has been returned";

        var rentalBillInput = new RentalBillCalculatorInput(rentedAt, returnedAt, rentalPricePerDay, insurancePricePerDay);

        var rentalBill = rentalBillCalculatorService.CalculateBill(rentalBillInput);

        var content = rentalReturnConfirmedTemplate.Template
            .Replace("{{make}}", make)
            .Replace("{{model}}", model)
            .Replace("{{name}}", toName)
            .Replace("{{numberOfDays}}", rentalBill.NumberOfDays.ToString())
            .Replace("{{insurancePricePerDay}}", insurancePricePerDay.ToString())
            .Replace("{{rentalPricePerDay}}", rentalPricePerDay.ToString())
            .Replace("{{totalInsurancePrice}}", rentalBill.InsuranceTotalPrice.ToString())
            .Replace("{{totalRentalPrice}}", rentalBill.RentalTotalPrice.ToString())
            .Replace("{{totalPrice}}", rentalBill.SummaryTotalPrice.ToString());

        var input = new SendEmailInput(fromEmail, fromName, toEmail, toName, subject, content, true);

        return input;

    }

    public SendEmailInput GenerateRentalReturnStartedTemplate(
        string toEmail,
        string toName,
        string make,
        string model)
    {
        string fromEmail = sendEmailOptions.FromEmail;

        string fromName = sendEmailOptions.FromName;

        string subject = "Your rental return has started";

        var content = rentalReturnStartedTemplate.Content
            .Replace("{{name}}", toName)
            .Replace("{{make}}", make)
            .Replace("{{model}}", model);

        var input = new SendEmailInput(fromEmail, fromName, toEmail, toName, subject, content, true);

        return input;

    }

}