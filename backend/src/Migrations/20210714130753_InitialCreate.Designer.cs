﻿// <auto-generated />
using System;
using BenefitsApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BenefitsApp.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210714130753_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("BenefitsApp.Data.Models.Dependent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Dependents");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c973769f-6bbf-47b1-8504-64929d6d50db"),
                            EmployeeId = new Guid("95fe2082-7726-4cb3-bcc9-fc27f0135853"),
                            FirstName = "Anthony Jr.",
                            LastName = "Soprano"
                        },
                        new
                        {
                            Id = new Guid("6fe0f280-a56e-4973-8ef5-8fe623889226"),
                            EmployeeId = new Guid("95fe2082-7726-4cb3-bcc9-fc27f0135853"),
                            FirstName = "Meadow",
                            LastName = "Soprano"
                        },
                        new
                        {
                            Id = new Guid("4e215c2b-4631-476d-8092-158640372295"),
                            EmployeeId = new Guid("95fe2082-7726-4cb3-bcc9-fc27f0135853"),
                            FirstName = "Carmela",
                            LastName = "Soprano"
                        },
                        new
                        {
                            Id = new Guid("c6ff219f-3f4e-4d8e-b3d2-fe03fb9066d3"),
                            EmployeeId = new Guid("9718d5b6-9a69-451a-adc7-d6c763976e24"),
                            FirstName = "Kelli",
                            LastName = "Moltisanti"
                        });
                });

            modelBuilder.Entity("BenefitsApp.Data.Models.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EnrollmentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EnrollmentId");

                    b.ToTable("Discounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("007c9870-cf16-443a-9438-f0128016ac95"),
                            EnrollmentId = new Guid("40e6437f-3093-4280-8c12-c5f74f547a52"),
                            Name = "NameDiscount"
                        },
                        new
                        {
                            Id = new Guid("c9a49c7a-eea8-40c5-be27-36d60bb0d031"),
                            EnrollmentId = new Guid("e303f29c-73b5-47e9-bc58-2a72199ecacf"),
                            Name = "NameDiscount"
                        });
                });

            modelBuilder.Entity("BenefitsApp.Data.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Salary")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("95fe2082-7726-4cb3-bcc9-fc27f0135853"),
                            FirstName = "Anthony",
                            LastName = "Soprano",
                            Salary = 52000m,
                            StartDate = new DateTime(2021, 4, 15, 8, 7, 53, 489, DateTimeKind.Local).AddTicks(5410)
                        },
                        new
                        {
                            Id = new Guid("9718d5b6-9a69-451a-adc7-d6c763976e24"),
                            FirstName = "Christopher",
                            LastName = "Moltisanti",
                            Salary = 52000m,
                            StartDate = new DateTime(2021, 5, 15, 8, 7, 53, 502, DateTimeKind.Local).AddTicks(1290)
                        });
                });

            modelBuilder.Entity("BenefitsApp.Data.Models.Enrollment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Benefit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Enrollments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("40e6437f-3093-4280-8c12-c5f74f547a52"),
                            Benefit = "GenericBenefit",
                            EmployeeId = new Guid("95fe2082-7726-4cb3-bcc9-fc27f0135853")
                        },
                        new
                        {
                            Id = new Guid("e303f29c-73b5-47e9-bc58-2a72199ecacf"),
                            Benefit = "GenericBenefit",
                            EmployeeId = new Guid("9718d5b6-9a69-451a-adc7-d6c763976e24")
                        });
                });

            modelBuilder.Entity("BenefitsApp.Data.Models.Dependent", b =>
                {
                    b.HasOne("BenefitsApp.Data.Models.Employee", "Employee")
                        .WithMany("Dependents")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("BenefitsApp.Data.Models.Discount", b =>
                {
                    b.HasOne("BenefitsApp.Data.Models.Enrollment", "Enrollment")
                        .WithMany("Discounts")
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enrollment");
                });

            modelBuilder.Entity("BenefitsApp.Data.Models.Enrollment", b =>
                {
                    b.HasOne("BenefitsApp.Data.Models.Employee", "Employee")
                        .WithMany("Enrollments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("BenefitsApp.Data.Models.Employee", b =>
                {
                    b.Navigation("Dependents");

                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("BenefitsApp.Data.Models.Enrollment", b =>
                {
                    b.Navigation("Discounts");
                });
#pragma warning restore 612, 618
        }
    }
}
