using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Provider.Persistence.Configurations;

public sealed class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .HasMaxLength(ConfigurationConstants.MakeConstants.NameMaxLength)
            .IsRequired();

        builder.Property(m => m.NumberOfDoors)
            .HasDefaultValue(ConfigurationConstants.ModelConstants.NumberOfDoorsDefaultValue)
            .IsRequired();

        builder.Property(m => m.NumberOfSeats)
            .HasDefaultValue(ConfigurationConstants.ModelConstants.NumberOfSeatsDefaultValue)
            .IsRequired();

        builder.Property(m => m.EngineType)
            .HasDefaultValue(EngineType.InternalCombustion)
            .IsRequired();

        builder.Property(m => m.WheelDriveType)
            .HasDefaultValue(WheelDriveType.AWD)
            .IsRequired();

        builder.HasOne(m => m.Make)
            .WithMany(mk => mk.Models)
            .HasForeignKey(m => m.MakeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(m => m.Segment)
            .WithMany(s => s.Models)
            .HasForeignKey(m => m.SegmentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(m => m.Cars)
            .WithOne(c => c.Model)
            .HasForeignKey(c => c.ModelId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}