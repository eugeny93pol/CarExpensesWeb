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
    [Migration("20210701120345_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CE.DataAccess.Models.Car", b =>
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

            modelBuilder.Entity("CE.DataAccess.Models.CarAction", b =>
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

            modelBuilder.Entity("CE.DataAccess.Models.CarActionType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("CarActionTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4795cc2c-eaea-4c14-a8ac-c420f5c52059"),
                            Name = "mileage"
                        },
                        new
                        {
                            Id = new Guid("9065f0b4-4324-456e-9e80-321e1b069940"),
                            Name = "purchases"
                        },
                        new
                        {
                            Id = new Guid("1d277e04-415b-4f34-83b8-0de33fd8a836"),
                            Name = "refill"
                        },
                        new
                        {
                            Id = new Guid("3157e000-3673-4da5-97c4-a2f1b838991e"),
                            Name = "repair"
                        });
                });

            modelBuilder.Entity("CE.DataAccess.Models.CarSettings", b =>
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

            modelBuilder.Entity("CE.DataAccess.Models.Role", b =>
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
                            Id = new Guid("a2eb62cd-259f-46e0-94a9-8e419c550f55"),
                            Name = "admin"
                        },
                        new
                        {
                            Id = new Guid("836188c0-4a07-42d8-b724-96924477680a"),
                            Name = "user"
                        });
                });

            modelBuilder.Entity("CE.DataAccess.Models.User", b =>
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

            modelBuilder.Entity("CE.DataAccess.Models.UserSettings", b =>
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

            modelBuilder.Entity("CE.DataAccess.Models.Car", b =>
                {
                    b.HasOne("CE.DataAccess.Models.User", null)
                        .WithMany("Cars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CE.DataAccess.Models.CarAction", b =>
                {
                    b.HasOne("CE.DataAccess.Models.Car", null)
                        .WithMany("Actions")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CE.DataAccess.Models.CarActionType", null)
                        .WithMany("Actions")
                        .HasForeignKey("Type")
                        .HasPrincipalKey("Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CE.DataAccess.Models.CarSettings", b =>
                {
                    b.HasOne("CE.DataAccess.Models.Car", null)
                        .WithOne("Settings")
                        .HasForeignKey("CE.DataAccess.Models.CarSettings", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CE.DataAccess.Models.User", b =>
                {
                    b.HasOne("CE.DataAccess.Models.Role", null)
                        .WithMany("Users")
                        .HasForeignKey("Role")
                        .HasPrincipalKey("Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CE.DataAccess.Models.UserSettings", b =>
                {
                    b.HasOne("CE.DataAccess.Models.User", null)
                        .WithOne("Settings")
                        .HasForeignKey("CE.DataAccess.Models.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CE.DataAccess.Models.Car", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("CE.DataAccess.Models.CarActionType", b =>
                {
                    b.Navigation("Actions");
                });

            modelBuilder.Entity("CE.DataAccess.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CE.DataAccess.Models.User", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Settings");
                });
#pragma warning restore 612, 618
        }
    }
}