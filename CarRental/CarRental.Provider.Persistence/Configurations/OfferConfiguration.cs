using CarRental.Common.Core.ProviderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Provider.Persistence.Configurations;

public sealed class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(o => o.GeneratedAt)
            .IsRequired();

        builder.Property(o => o.ExpiresAt)
            .IsRequired();

        builder.Property(o => o.RentalPricePerDay)
            .HasColumnType(ConfigurationConstants.PriceConstants.DatabaseType)
            .IsRequired();

        builder.Property(o => o.InsurancePricePerDay)
            .HasColumnType(ConfigurationConstants.PriceConstants.DatabaseType)
            .IsRequired();

        builder.HasOne(o => o.Car)
            .WithMany(c => c.Offers)
            .HasForeignKey(o => o.CarId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(o => o.Rental)
            .WithOne(r => r.Offer)
            .HasForeignKey<Rental>(r => r.OfferId)
            .IsRequired(false);
    }
}