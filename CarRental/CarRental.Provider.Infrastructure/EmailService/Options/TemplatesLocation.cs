namespace CarRental.Provider.Infrastructure.EmailService.Options;

public sealed class TemplatesLocation
{
    public string OfferConfirmedTemplate { get; set; } = string.Empty;

    public string RentalConfirmedTemplate {  get; set; } = string.Empty;

    public string RentalReturnedTemplate {  get; set; } = string.Empty;

    public string RentalReturnedStarted {  get; set; } = string.Empty;

}
