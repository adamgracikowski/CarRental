namespace CarRental.Comparer.Persistence.Options;

public sealed class ConnectionStringsOptions
{
    public const string SectionName = "ConnectionsStrings";
    public string DefaultConnection { get; set; } = string.Empty;
}
