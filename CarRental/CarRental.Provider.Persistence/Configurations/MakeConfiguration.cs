using CarRental.Common.Core.ProviderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Provider.Persistence.Configurations;

public sealed class MakeConfiguration : IEntityTypeConfiguration<Make>
{
    public void Configure(EntityTypeBuilder<Make> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(ConfigurationConstants.MakeConstants.NameMaxLength)
            .IsRequired();

        builder.HasMany(mk => mk.Models)
            .WithOne(m => m.Make)
            .HasForeignKey(m => m.MakeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}