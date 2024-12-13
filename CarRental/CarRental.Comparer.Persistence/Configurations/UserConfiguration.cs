using CarRental.Common.Core.ComparerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Comparer.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(u => u.Id);

		builder.Property(u => u.Email)
			.IsRequired()
			.HasMaxLength(ConfigurationConstants.UserConstants.EmailMaxLength);

		builder.Property(u => u.Name)
			.IsRequired()
			.HasMaxLength(ConfigurationConstants.UserConstants.NameMaxLength);

		builder.Property(u => u.Lastname)
			.IsRequired()
			.HasMaxLength(ConfigurationConstants.UserConstants.LastNameMaxLength);

		builder.Property(u => u.Birthday)
			.IsRequired();

		builder.Property(u => u.DrivingLicenseDate)
			.IsRequired();

		builder.Property(u => u.Longitude)
			.HasPrecision(ConfigurationConstants.LocalizationConstants.LongitudePrecision, ConfigurationConstants.LocalizationConstants.LongitudeScale)
			.IsRequired();

		builder.Property(u => u.Latitude)
			.HasPrecision(ConfigurationConstants.LocalizationConstants.LatitudePrecision, ConfigurationConstants.LocalizationConstants.LatitudeScale)
			.IsRequired();

		builder.HasMany(u => u.RentalTransactions)
			   .WithOne(rt => rt.User)
			   .HasForeignKey(rt => rt.UserId)
			   .OnDelete(DeleteBehavior.Cascade);

		builder.ToTable(tb => tb.HasCheckConstraint(
				"CK_User_Longitude",
				$"{nameof(User.Longitude)} >= {ConfigurationConstants.LocalizationConstants.LongitudeMin} " +
				$"AND {nameof(User.Longitude)} <= {ConfigurationConstants.LocalizationConstants.LongitudeMax}")
			);

		builder.ToTable(tb => tb.HasCheckConstraint(
			"CK_User_Latitude",
			$"{nameof(User.Latitude)} >= {ConfigurationConstants.LocalizationConstants.LatitudeMin} " +
			$"AND {nameof(User.Latitude)} <= {ConfigurationConstants.LocalizationConstants.LatitudeMax}")
		);
	}
}