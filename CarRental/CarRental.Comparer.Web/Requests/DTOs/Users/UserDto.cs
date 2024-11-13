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

    [Range(UserDtoValidationConstants.AgeConstants.LowerAge,
        UserDtoValidationConstants.AgeConstants.UpperAge,
        ErrorMessage = UserDtoValidationConstants.AgeConstants.AgeRange)]
    public int Age { get; set; }

    [Range(UserDtoValidationConstants.DrivingLicenseYearsConstants.LowerYears,
        UserDtoValidationConstants.DrivingLicenseYearsConstants.UpperYears,
        ErrorMessage = UserDtoValidationConstants.DrivingLicenseYearsConstants.DrivingLicenseYearsRange)]
    public int DrivingLicenseYears { get; set; }

    [Required(ErrorMessage = UserDtoValidationConstants.PositionConstants.PositionIsRequired)]
    public double Longitude { get; set; }

    [Required(ErrorMessage = UserDtoValidationConstants.PositionConstants.PositionIsRequired)]
    public double Latitude { get; set; }
}
