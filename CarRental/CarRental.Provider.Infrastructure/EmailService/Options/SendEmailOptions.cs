using System.Globalization;

namespace CarRental.Provider.Infrastructure.EmailService.Options;

public sealed class SendEmailOptions
{
    public static string SectionName = "SendGrid";

    public string APIKey { get; set; } = string.Empty;

    public string FromName { get; set; } = string.Empty;

    public string FromEmail { get; set; } = string.Empty;

    public string LinkTemplate {  get; set; } = string.Empty;

    public TemplatesLocation TemplatesLocation { get; set; } = new TemplatesLocation();
}
