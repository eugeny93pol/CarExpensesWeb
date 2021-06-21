﻿// <auto-generated />
using System;
using CE.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CE.Repository.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210611120929_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CE.DataAccess.ActionType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("ActionTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("16ab5101-3894-4d31-82b9-1974410fab4f"),
                            Name = "mileage"
                        },
                        new
                        {
                            Id = new Guid("22ac6a73-86ac-4a19-a70f-680b71c8b0e6"),
                            Name = "purchases"
                        },
                        new
                        {
                            Id = new Guid("bf9216fd-dd03-4c4e-8719-0841b68f5f6f"),
                            Name = "refill"
                        },
                        new
                        {
                            Id = new Guid("db8a37e2-57db-486b-8a67-0867dfab1a67"),
                            Name = "repair"
                        });
                });

            modelBuilder.Entity("CE.DataAccess.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Vin")
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CE.DataAccess.CarAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("money");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Date")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Mileage")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("Type");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("CE.DataAccess.CarSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MeasurementSystem")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId")
                        .IsUnique();

                    b.ToTable("CarsSettings");
                });

            modelBuilder.Entity("CE.DataAccess.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3044c5e7-e87c-4c25-a2a4-5c3c254250c0"),
                            Name = "admin"
                        },
                        new
                        {
                            Id = new Guid("ccc8575e-0af1-4c27-82c8-5caa1f50d8b4"),
                            Name = "user"
                        });
                });

            modelBuilder.Entity("CE.DataAccess.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Role");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CE.DataAccess.UserSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DefaultCarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeasurementSystem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Theme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UsersSettings");
                });

            modelBuilder.Entity("CE.DataAccess.Car", b =>
                {
                    b.HasOne("CE.DataAccess.User", null)
                        .WithMany("Cars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CE.DataAccess.CarAction", b =>
                {
                    b.HasOne("CE.DataAccess.Car", null)
                        .WithMany("Actions")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CE.DataAccess.ActionType", null)
                        .WithMany("Actions")
                        .HasForeignKey("Type")
                        .HasPrincipalKey("Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CE.DataAccess.CarSettings", b =>
                {
                    b.HasOne("CE.DataAccess.Car", null)
                        .WithOne("Settings")
                        .HasForeignKey("CE.DataAccess.CarSettings", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CE.DataAccess.User", b =>
                {
                    b.HasOne("CE.DataAccess.Role", null)
                        .WithMany("Users")
                        .HasForeignKey("Role")
                        .HasPrincipalKey("Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CE.DataAccess.UserSettings", b =>
                {
                    b.HasOne("CE.DataAccess.User", null)
                        .WithOne("Settings")
                        .HasForeignKey("CE.DataAccess.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CE.DataAccess.ActionType", b =>
                {
                    b.Navigation("Actions");
                });

            modelBuilder.Entity("CE.DataAccess.Car", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("CE.DataAccess.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CE.DataAccess.User", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Settings");
                });
#pragma warning restore 612, 618
        }
    }
}