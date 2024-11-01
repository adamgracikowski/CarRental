using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Provider.Persistence.Configurations;

public sealed class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Status)
            .HasDefaultValue(RentalStatus.Unconfirmed)
            .IsRequired();

        builder.HasOne(r => r.Offer)
            .WithOne(o => o.Rental)
            .HasForeignKey<Rental>(r => r.OfferId)
            .IsRequired();

        builder.HasOne(r => r.Customer)
            .WithMany(cu => cu.Rentals)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(r => r.RentalReturn)
            .WithOne(rr => rr.Rental)
            .HasForeignKey<RentalReturn>(rr => rr.RentalId)
            .IsRequired(false);
    }
}