using CarRental.Common.Core.ComparerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Comparer.Persistence.Configurations;

public sealed class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(ConfigurationConstants.ProviderConstants.NameMaxLength);

        builder.HasMany(p => p.RentalTransactions)
            .WithOne(rt => rt.Provider)
            .HasForeignKey(rt => rt.ProviderId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(p => p.Employees)
            .WithOne(e => e.Provider)
            .HasForeignKey(e => e.ProviderId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

    }
}
