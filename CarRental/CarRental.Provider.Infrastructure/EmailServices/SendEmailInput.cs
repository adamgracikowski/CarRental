namespace CarRental.Provider.Infrastructure.EmailServices;

public sealed record SendEmailInput(
	string FromEmail,
	string FromName,
	string ToEmail,
	string ToName,
	string Subject,
	string Content,
	bool IsHtml
);