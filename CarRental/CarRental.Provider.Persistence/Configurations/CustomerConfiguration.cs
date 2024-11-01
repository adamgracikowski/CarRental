using CarRental.Common.Core.ProviderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Provider.Persistence.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(cu => cu.Id);

        builder.Property(cu => cu.EmailAddress)
            .HasMaxLength(ConfigurationConstants.CustomerConstants.EmailAddressMaxLength)
            .IsRequired();

        builder.Property(cu => cu.FirstName)
            .HasMaxLength(ConfigurationConstants.CustomerConstants.FirstNameMaxLength)
            .IsRequired();

        builder.Property(cu => cu.LastName)
            .HasMaxLength(ConfigurationConstants.CustomerConstants.LastNameMaxLength)
            .IsRequired();

        builder.HasMany(cu => cu.Rentals)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasIndex(cu => cu.EmailAddress)
            .IsUnique();
    }
}