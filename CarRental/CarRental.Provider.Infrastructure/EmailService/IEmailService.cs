namespace CarRental.Provider.Infrastructure.EmailService;

public interface IEmailService
{
    public Task<SendEmailResult> SendEmailAsync(SendEmailInput sendEmailInput);
}