using CarRental.Common.Core.ProviderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Provider.Persistence.Configurations;

public sealed class InsuranceConfiguration : IEntityTypeConfiguration<Insurance>
{
    public void Configure(EntityTypeBuilder<Insurance> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .HasMaxLength(ConfigurationConstants.InsuranceConstants.NameMaxLength)
            .IsRequired();

        builder.Property(i => i.Description)
            .HasMaxLength(ConfigurationConstants.InsuranceConstants.DescriptionMaxLength)
            .IsRequired(false);

        builder.Property(i => i.PricePerDay)
            .HasColumnType(ConfigurationConstants.PriceConstants.DatabaseType)
            .IsRequired();

        builder.HasMany(i => i.Segments)
            .WithOne(s => s.Insurance)
            .HasForeignKey(s => s.InsuranceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}