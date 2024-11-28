namespace CarRental.Common.Core.ComparerEntities;

public sealed class User : EntityBase
{
    public string Email { get; set; } 

    public string Name { get; set; }

    public string Lastname { get; set; }

    public int Age { get; set; }

    public int DrivingLicenseYears { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public ICollection<RentalTransaction> RentalTransactions { get; set; }
}
