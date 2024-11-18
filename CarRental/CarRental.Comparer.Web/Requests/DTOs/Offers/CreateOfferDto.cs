namespace CarRental.Comparer.Web.Requests.DTOs.Offers;

public sealed class CreateOfferDto
{
	public CreateOfferDto()
	{
		
	}
	public CreateOfferDto(int drivingLicenseYears, int age, decimal latitude, decimal longitude)
	{
		DrivingLicenseYears = drivingLicenseYears;
		Age = age;
		Latitude = latitude;
		Longitude = longitude;
	}

	public int DrivingLicenseYears { get; set; }
    public int Age { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}