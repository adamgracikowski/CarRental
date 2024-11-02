using CarRental.Common.Core.ComparerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Comparer.Persistence.Configurations;

public sealed class RentalTransactionConfiguration : IEntityTypeConfiguration<RentalTransaction>
{
    public void Configure(EntityTypeBuilder<RentalTransaction> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.RentalOuterId)
            .HasMaxLength(ConfigurationConstants.RentalTransactionConstants.RentalOuterIdMaxLength)
            .IsRequired();

        builder.Property(rt => rt.RentalPricePerDay)
            .HasColumnType(ConfigurationConstants.RentalTransactionConstants.DecimalPrecision)
            .IsRequired();

        builder.Property(rt => rt.InsurancePricePerDay)
            .HasColumnType(ConfigurationConstants.RentalTransactionConstants.DecimalPrecision)
            .IsRequired();

        builder.Property(rt => rt.RentedAt)
            .IsRequired();

        builder.Property(rt => rt.ReturnedAt)
            .IsRequired(false);

        builder.Property(rt => rt.Status)
            .HasDefaultValue(ConfigurationConstants.RentalTransactionConstants.DefaultRentalStatus)
            .IsRequired();

        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RentalTransactions)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rt => rt.Provider)
            .WithMany(p => p.RentalTransactions)
            .HasForeignKey(rt => rt.ProviderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rt => rt.CarDetails)
            .WithMany(cd => cd.RentalTransactions)
            .HasForeignKey(rt => rt.CarDetailsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
