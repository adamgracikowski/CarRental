﻿// <auto-generated />
using System;
using CarRental.Provider.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRental.Provider.Persistence.Migrations
{
	[DbContext(typeof(CarRentalProviderDbContext))]
    partial class CarRentalProviderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FuelType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<decimal>("Latitude")
                        .HasPrecision(8, 6)
                        .HasColumnType("decimal(8,6)");

                    b.Property<decimal>("Longitude")
                        .HasPrecision(9, 6)
                        .HasColumnType("decimal(9,6)");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("TransmissionType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Cars", t =>
                        {
                            t.HasCheckConstraint("CK_Car_Latitude", "Latitude >= -90 AND Latitude <= 90");

                            t.HasCheckConstraint("CK_Car_Longitude", "Longitude >= -180 AND Longitude <= 180");
                        });
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmailAddress")
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

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Insurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Make", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Makes");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EngineType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("MakeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumberOfDoors")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(4);

                    b.Property<int>("NumberOfSeats")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(5);

                    b.Property<int>("SegmentId")
                        .HasColumnType("int");

                    b.Property<int>("WheelDriveType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.HasIndex("SegmentId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GeneratedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("GeneratedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("InsurancePricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int?>("RentalId")
                        .HasColumnType("int");

                    b.Property<decimal>("RentalPricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<int?>("RentalReturnId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OfferId")
                        .IsUnique();

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.RentalReturn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Image")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Latitude")
                        .HasPrecision(8, 6)
                        .HasColumnType("decimal(8,6)");

                    b.Property<decimal>("Longitude")
                        .HasPrecision(9, 6)
                        .HasColumnType("decimal(9,6)");

                    b.Property<int>("RentalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturnedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RentalId")
                        .IsUnique();

                    b.ToTable("RentalReturns", t =>
                        {
                            t.HasCheckConstraint("CK_RentalReturn_Latitude", "Latitude >= -90 AND Latitude <= 90");

                            t.HasCheckConstraint("CK_RentalReturn_Longitude", "Longitude >= -180 AND Longitude <= 180");
                        });
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Segment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("InsuranceId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("InsuranceId");

                    b.ToTable("Segments");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Car", b =>
                {
                    b.HasOne("CarRental.Common.Core.ProviderEntities.Model", "Model")
                        .WithMany("Cars")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Model", b =>
                {
                    b.HasOne("CarRental.Common.Core.ProviderEntities.Make", "Make")
                        .WithMany("Models")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental.Common.Core.ProviderEntities.Segment", "Segment")
                        .WithMany("Models")
                        .HasForeignKey("SegmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Make");

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Offer", b =>
                {
                    b.HasOne("CarRental.Common.Core.ProviderEntities.Car", "Car")
                        .WithMany("Offers")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Rental", b =>
                {
                    b.HasOne("CarRental.Common.Core.ProviderEntities.Customer", "Customer")
                        .WithMany("Rentals")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental.Common.Core.ProviderEntities.Offer", "Offer")
                        .WithOne("Rental")
                        .HasForeignKey("CarRental.Common.Core.ProviderEntities.Rental", "OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.RentalReturn", b =>
                {
                    b.HasOne("CarRental.Common.Core.ProviderEntities.Rental", "Rental")
                        .WithOne("RentalReturn")
                        .HasForeignKey("CarRental.Common.Core.ProviderEntities.RentalReturn", "RentalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rental");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Segment", b =>
                {
                    b.HasOne("CarRental.Common.Core.ProviderEntities.Insurance", "Insurance")
                        .WithMany("Segments")
                        .HasForeignKey("InsuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Insurance");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Car", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Customer", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Insurance", b =>
                {
                    b.Navigation("Segments");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Make", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Model", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Offer", b =>
                {
                    b.Navigation("Rental");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Rental", b =>
                {
                    b.Navigation("RentalReturn");
                });

            modelBuilder.Entity("CarRental.Common.Core.ProviderEntities.Segment", b =>
                {
                    b.Navigation("Models");
                });
#pragma warning restore 612, 618
        }
    }
}
