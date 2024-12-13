using CarRental.Common.Core.ComparerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Comparer.Persistence.Configurations;

public sealed class CarDetailsConfiguration : IEntityTypeConfiguration<CarDetails>
{
    public void Configure(EntityTypeBuilder<CarDetails> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(c => c.OuterId)
            .IsRequired();

        builder.Property(c => c.Make)
            .IsRequired();

        builder.Property(c => c.Model)
            .IsRequired();

        builder.Property(c => c.Segment)
            .IsRequired(false);

        builder.Property(c => c.FuelType)
           .IsRequired(false);

        builder.Property(c => c.TransmissionType)
            .IsRequired(false);

        builder.Property(c => c.YearOfProduction)
            .IsRequired();

        builder.Property(c => c.NumberOfDoors)
            .IsRequired(false);

        builder.Property(c => c.NumberOfSeats)
            .IsRequired(false);

        builder.HasMany(c => c.RentalTransactions)
            .WithOne(o => o.CarDetails)
            .HasForeignKey(o => o.CarDetailsId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}