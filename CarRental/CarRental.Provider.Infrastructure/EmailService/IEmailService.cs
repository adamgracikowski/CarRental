namespace CarRental.Provider.Infrastructure.EmailService;

public interface IEmailService
{
    Task<SendEmailResult> SendEmailAsync(SendEmailInput sendEmailInput);
}