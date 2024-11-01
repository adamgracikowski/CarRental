using CarRental.Common.Core.ProviderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Provider.Persistence.Configurations;

public sealed class SegmentConfiguration : IEntityTypeConfiguration<Segment>
{
    public void Configure(EntityTypeBuilder<Segment> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(ConfigurationConstants.SegmentConstants.NameMaxLength)
            .IsRequired();

        builder.Property(s => s.Description)
            .HasMaxLength(ConfigurationConstants.InsuranceConstants.DescriptionMaxLength)
            .IsRequired(false);

        builder.Property(s => s.PricePerDay)
            .HasColumnType(ConfigurationConstants.PriceConstants.DatabaseType)
            .IsRequired();

        builder.HasOne(s => s.Insurance)
            .WithMany(i => i.Segments)
            .HasForeignKey(s => s.InsuranceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(s => s.Models)
            .WithOne(m => m.Segment)
            .HasForeignKey(m => m.SegmentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}