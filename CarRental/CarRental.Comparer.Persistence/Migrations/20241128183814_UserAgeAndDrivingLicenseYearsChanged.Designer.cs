﻿// <auto-generated />
using System;
using CarRental.Comparer.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRental.Comparer.Persistence.Migrations
{
    [DbContext(typeof(CarRentalComparerDbContext))]
    [Migration("20241128183814_UserAgeAndDrivingLicenseYearsChanged")]
    partial class UserAgeAndDrivingLicenseYearsChanged
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarRental.Common.Core.ComparerEntities.CarDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FuelType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberOfDoors")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<string>("OuterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Segment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransmissionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfProduction")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CarDetails");
                });

            modelBuilder.Entity("CarRental.Common.Core.ComparerEntities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CarRental.Common.Core.ComparerEntities.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("CarRental.Common.Core.ComparerEntities.RentalTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarDetailsId")
                        .HasColumnType("int");

                    b.Property<decimal>("InsurancePricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.Property<string>("RentalOuterId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("RentalPricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RentedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReturnedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarDetailsId");

                    b.HasIndex("ProviderId");

                    b.HasIndex("UserId");

                    b.ToTable("RentalTransactions");
                });

            modelBuilder.Entity("CarRental.Common.Core.ComparerEntities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DrivingLicenseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Latitude")
                        .HasPrecision(8, 6)
                        .HasColumnType("float(8)");

                    b.Property<double>("Longitude")
                        .HasPrecision(9, 6)
                        .HasColumnType("float(9)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", t =>
                        {
                            t.HasCheckConstraint("CK_User_Latitude", "Latitude >= -90 AND Latitude <= 90");

                            t.HasCheckConstraint("CK_User_Longitude", "Longitude >= -180 AND Longitude <= 180");
                        });
                });

            modelBuilder.Entity("CarRental.Common.Core.ComparerEntities.Employee", b =>
                {
                    b.HasOne("CarRental.Common.Core.ComparerEntities.Provider", "Provider")
                        .WithMany("Employees")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("CarRental.Common.Core.ComparerEntities.RentalTransaction", b =>
                {
                    b.HasOne("CarRental.Common.Core.ComparerEntities.CarDetails", "CarDetails")
                        .WithMany("RentalTransactions")
                        .HasForeignKey("CarDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental.Common.Core.ComparerEntities.Provider", "Provider")
                        .WithMany("RentalTransactions")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental.Common.Core.ComparerEntities.User", "User")
                        .WithMany("RentalTransactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarDetails");

                    b.Navigation("Provider");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarRental.Common.Core.ComparerEntities.CarDetails", b =>
                {
                    b.Navigation("RentalTransactions");
                });

            modelBuilder.Entity("CarRental.Common.Core.ComparerEntities.Provider", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("RentalTransactions");
                });

            modelBuilder.Entity("CarRental.Common.Core.ComparerEntities.User", b =>
                {
                    b.Navigation("RentalTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
