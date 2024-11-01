using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Provider.Persistence.Configurations;

public sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(c => c.ProductionYear)
            .IsRequired();

        builder.Property(c => c.FuelType)
            .HasDefaultValue(FuelType.Gasoline)
            .IsRequired();

        builder.Property(c => c.TransmissionType)
            .HasDefaultValue(TransmissionType.Manual)
            .IsRequired();

        builder.Property(c => c.Status)
            .HasDefaultValue(CarStatus.Available)
            .IsRequired();

        builder.Property(c => c.Longitude)
            .HasPrecision(ConfigurationConstants.LocalizationConstants.LongitudePrecision, ConfigurationConstants.LocalizationConstants.LongitudeScale)
            .IsRequired();

        builder.Property(c => c.Latitude)
            .HasPrecision(ConfigurationConstants.LocalizationConstants.LatitudePrecision, ConfigurationConstants.LocalizationConstants.LatitudeScale)
            .IsRequired();

        builder.HasOne(c => c.Model)
            .WithMany(m => m.Cars)
            .HasForeignKey(c => c.ModelId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(c => c.Offers)
            .WithOne(o => o.Car)
            .HasForeignKey(o => o.CarId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.ToTable(tb => tb.HasCheckConstraint(
            "CK_Car_Longitude",
            $"{nameof(Car.Longitude)} >= {ConfigurationConstants.LocalizationConstants.LongitudeMin} AND {nameof(Car.Longitude)} <= {ConfigurationConstants.LocalizationConstants.LongitudeMax}")
        );

        builder.ToTable(tb => tb.HasCheckConstraint(
            "CK_Car_Latitude",
            $"{nameof(Car.Latitude)} >= {ConfigurationConstants.LocalizationConstants.LatitudeMin} AND {nameof(Car.Latitude)} <= {ConfigurationConstants.LocalizationConstants.LatitudeMax}")
        );
    }
}