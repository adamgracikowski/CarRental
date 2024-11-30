using System.ComponentModel.DataAnnotations;
using CarRental.Comparer.Web.Requests.ValidationConstants;

namespace CarRental.Comparer.Web.Requests.DTOs.Users;

public sealed class UserDto
{
	[Required(ErrorMessage = UserDtoValidationConstants.EmailConstants.EmailIsRequired)]
	[EmailAddress(ErrorMessage = UserDtoValidationConstants.EmailConstants.EmailErrorMessage)]
	[StringLength(UserDtoValidationConstants.EmailConstants.MaxLength,
		ErrorMessage = UserDtoValidationConstants.EmailConstants.EmailMaxLength)]
	public string Email { get; set; }

	[Required(ErrorMessage = UserDtoValidationConstants.NameConstants.FirstNameIsRequired)]
	[StringLength(UserDtoValidationConstants.NameConstants.MaxLength,
		ErrorMessage = UserDtoValidationConstants.NameConstants.FirstNameMaxLength)]
	public string Name { get; set; }

	[Required(ErrorMessage = UserDtoValidationConstants.LastNameConstants.LastNameIsRequired)]
	[StringLength(UserDtoValidationConstants.LastNameConstants.MaxLength,
		ErrorMessage = UserDtoValidationConstants.LastNameConstants.LastNameMaxLength)]
	public string Lastname { get; set; }

	[Required(ErrorMessage = "Birthdate is required.")]
	[CustomValidation(typeof(UserDtoValidator), nameof(UserDtoValidator.ValidateBirthdate))]
	public DateTime? Birthday { get; set; }

	[Required(ErrorMessage = "Driving license date is required.")]
	[CustomValidation(typeof(UserDtoValidator), nameof(UserDtoValidator.ValidateDrivingLicenseDate))]
	public DateTime? DrivingLicenseDate { get; set; }

	[Required(ErrorMessage = UserDtoValidationConstants.PositionConstants.PositionIsRequired)]
	[Range(UserDtoValidationConstants.PositionConstants.LongtitudeMin,
		UserDtoValidationConstants.PositionConstants.LongtitudeMax,
		ErrorMessage = UserDtoValidationConstants.PositionConstants.InvalidLongitude)]
	public double Longitude { get; set; }

	[Required(ErrorMessage = UserDtoValidationConstants.PositionConstants.PositionIsRequired)]
	[Range(UserDtoValidationConstants.PositionConstants.LatitudeMin,
		UserDtoValidationConstants.PositionConstants.LatitudeMax,
		ErrorMessage = UserDtoValidationConstants.PositionConstants.InvalidLatitude)]
	public double Latitude { get; set; }
}
