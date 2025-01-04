using CarRental.Provider.Infrastructure.Calculators.RentalBillCalculator;
using CarRental.Provider.Infrastructure.EmailServices.Options;
using CarRental.Provider.Infrastructure.EmailServices.TemplateProviders;
using Microsoft.Extensions.Options;

namespace CarRental.Provider.Infrastructure.EmailServices;

public sealed class EmailInputMaker : IEmailInputMaker
{
	private const string Make = "{{make}}";
	private const string Model = "{{model}}";
	private const string Name = "{{name}}";

	private const string OfferConfirmedSubject = "Confirm your car rental";
	private const string RentalConfirmedSubject = "Your car rental is confirmed";
	private const string RentalReturnedSubject = "Your rental has been returned";
	private const string RentalReturnStartedSubject = "Your rental return has started";

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
		var fromEmail = this.sendEmailOptions.FromEmail;
		var fromName = this.sendEmailOptions.FromName;

		var link = $"{this.sendEmailOptions.LinkTemplate}/Rentals/{rentalId}/confirm/{offerKey}";

		var content = this.offerConfirmedTemplate.Template
			.Replace(Make, make)
			.Replace(Model, model)
			.Replace(Name, toName)
			.Replace("{{confirmation_link}}", link);

		var input = new SendEmailInput(fromEmail, fromName, toEmail, toName, OfferConfirmedSubject, content, IsHtml: true);

		return input;

	}

	public SendEmailInput GenerateRentalConfirmedInput(
		string toEmail,
		string toName,
		string make,
		string model)
	{
		var fromEmail = this.sendEmailOptions.FromEmail;
		var fromName = this.sendEmailOptions.FromName;

		var content = this.rentalConfirmedTemplate.Template
			.Replace(Make, make)
			.Replace(Model, model)
			.Replace(Name, toName);

		var input = new SendEmailInput(fromEmail, fromName, toEmail, toName, RentalConfirmedSubject, content, IsHtml: true);

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
		var fromEmail = this.sendEmailOptions.FromEmail;
		var fromName = this.sendEmailOptions.FromName;

		var rentalBillInput = new RentalBillCalculatorInput(rentedAt, returnedAt, rentalPricePerDay, insurancePricePerDay);

		var rentalBill = this.rentalBillCalculatorService.CalculateBill(rentalBillInput);

		var content = this.rentalReturnConfirmedTemplate.Template
			.Replace(Make, make)
			.Replace(Model, model)
			.Replace(Name, toName)
			.Replace("{{numberOfDays}}", rentalBill.NumberOfDays.ToString())
			.Replace("{{insurancePricePerDay}}", insurancePricePerDay.ToString())
			.Replace("{{rentalPricePerDay}}", rentalPricePerDay.ToString())
			.Replace("{{totalInsurancePrice}}", rentalBill.InsuranceTotalPrice.ToString())
			.Replace("{{totalRentalPrice}}", rentalBill.RentalTotalPrice.ToString())
			.Replace("{{totalPrice}}", rentalBill.SummaryTotalPrice.ToString());

		var input = new SendEmailInput(fromEmail, fromName, toEmail, toName, RentalReturnedSubject, content, IsHtml: true);

		return input;

	}

	public SendEmailInput GenerateRentalReturnStartedTemplate(
		string toEmail,
		string toName,
		string make,
		string model)
	{
		var fromEmail = this.sendEmailOptions.FromEmail;
		var fromName = this.sendEmailOptions.FromName;

		var content = this.rentalReturnStartedTemplate.Content
			.Replace(Name, toName)
			.Replace(Make, make)
			.Replace(Model, model);

		var input = new SendEmailInput(fromEmail, fromName, toEmail, toName, RentalReturnStartedSubject, content, IsHtml: true);

		return input;
	}
}