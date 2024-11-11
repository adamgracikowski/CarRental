using SendGrid;
using SendGrid.Helpers.Mail;

namespace CarRental.Provider.Infrastructure.EmailService;

public sealed class EmailService: IEmailService
{
    private readonly SendGridClient client;
    public EmailService(SendGridClient client)
    {
        this.client = client;
    }
    public async Task<SendEmailResult> SendEmailAsync(SendEmailInput sendEmailInput)
    {
        var from = new EmailAddress(sendEmailInput.FromEmail, sendEmailInput.FromName);

        var to = new EmailAddress(sendEmailInput.ToEmail, sendEmailInput.ToName);

        var message = MailHelper.CreateSingleEmail(
            from,
            to,
            sendEmailInput.Subject,
            sendEmailInput.IsHtml ? null : sendEmailInput.Content,
            sendEmailInput.IsHtml ? sendEmailInput.Content : null
        );

        var result = await client.SendEmailAsync(message);

        if (result.IsSuccessStatusCode)
        {
            return new SendEmailResult(true, string.Empty);
        }

        var errorMessage = await result.Body.ReadAsStringAsync();

        return new SendEmailResult(false, errorMessage);
    }
}
