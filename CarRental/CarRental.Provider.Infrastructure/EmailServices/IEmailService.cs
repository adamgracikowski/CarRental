namespace CarRental.Provider.Infrastructure.EmailServices;

public interface IEmailService
{
	Task<SendEmailResult> SendEmailAsync(SendEmailInput sendEmailInput);
}