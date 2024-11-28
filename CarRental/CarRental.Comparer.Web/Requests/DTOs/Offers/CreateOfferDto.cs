using CarRental.Comparer.Web.Requests.ValidationConstants;
using System.ComponentModel.DataAnnotations;

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

	[Range(UserDtoValidationConstants.DrivingLicenseYearsConstants.LowerYears,
	CreateOfferDtoValidationConstants.DrivingLicenseYearsConstants.UpperYears,
	ErrorMessage = CreateOfferDtoValidationConstants.DrivingLicenseYearsConstants.DrivingLicenseYearsRange)]
	public int DrivingLicenseYears { get; set; }

	[Range(CreateOfferDtoValidationConstants.AgeConstants.LowerAge,
	CreateOfferDtoValidationConstants.AgeConstants.UpperAge,
	ErrorMessage = CreateOfferDtoValidationConstants.AgeConstants.AgeRange)]
	public int Age { get; set; }
	public decimal Latitude { get; set; }
	public decimal Longitude { get; set; }
}