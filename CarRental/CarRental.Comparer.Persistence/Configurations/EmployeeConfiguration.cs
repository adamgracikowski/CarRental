using CarRental.Common.Core.ComparerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Comparer.Persistence.Configurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(ConfigurationConstants.EmployeeConstants.EmailMaxLength);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(ConfigurationConstants.EmployeeConstants.NameMaxLength);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(ConfigurationConstants.EmployeeConstants.LastNameMaxLength);

        builder.Property(e => e.ProviderId)
            .IsRequired();

        builder.HasOne(e => e.Provider)
            .WithMany(p => p.Employees)
            .HasForeignKey(e => e.ProviderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}