using CarRental.Common.Core.ProviderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Provider.Persistence.Configurations;

public sealed class RentalReturnConfiguration : IEntityTypeConfiguration<RentalReturn>
{
    public void Configure(EntityTypeBuilder<RentalReturn> builder)
    {
        builder.HasKey(rr => rr.Id);

        builder.Property(rr => rr.ReturnedAt)
            .IsRequired();

        builder.Property(rr => rr.Description)
            .HasMaxLength(ConfigurationConstants.RentalReturnConstants.DescriptionMaxLength)
            .IsRequired(false);

        builder.Property(rr => rr.Image)
            .HasMaxLength(ConfigurationConstants.RentalReturnConstants.ImageMaxLength)
            .IsRequired(false);

        builder.Property(rr => rr.Longitude)
            .HasPrecision(ConfigurationConstants.LocalizationConstants.LongitudePrecision, ConfigurationConstants.LocalizationConstants.LongitudeScale)
            .IsRequired();

        builder.Property(rr => rr.Latitude)
            .HasPrecision(ConfigurationConstants.LocalizationConstants.LatitudePrecision, ConfigurationConstants.LocalizationConstants.LatitudeScale)
            .IsRequired();

        builder.HasOne(rr => rr.Rental)
            .WithOne(r => r.RentalReturn)
            .HasForeignKey<RentalReturn>(rr => rr.RentalId)
            .IsRequired();

        builder.ToTable(tb => tb.HasCheckConstraint(
            "CK_RentalReturn_Longitude",
            $"{nameof(RentalReturn.Longitude)} >= {ConfigurationConstants.LocalizationConstants.LongitudeMin} AND {nameof(RentalReturn.Longitude)} <= {ConfigurationConstants.LocalizationConstants.LongitudeMax}")
        );

        builder.ToTable(tb => tb.HasCheckConstraint(
            "CK_RentalReturn_Latitude",
            $"{nameof(RentalReturn.Latitude)} >= {ConfigurationConstants.LocalizationConstants.LatitudeMin} AND {nameof(RentalReturn.Latitude)} <= {ConfigurationConstants.LocalizationConstants.LatitudeMax}")
        );
    }
}