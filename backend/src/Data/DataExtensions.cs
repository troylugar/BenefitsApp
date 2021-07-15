using System;
using BenefitsApp.Business.Entities.Benefits;
using BenefitsApp.Business.Entities.Discounts;
using BenefitsApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BenefitsApp.Data
{
  public static class DataExtensions
  {
    public static ModelBuilder AddSopranos(this ModelBuilder modelBuilder)
    {
      var Tony = new Employee()
      {
        Id = Guid.NewGuid(),
        FirstName = "Anthony",
        LastName = "Soprano",
        Salary = 52_000m,
        StartDate = DateTime.Now.Subtract(TimeSpan.FromDays(90))
      };
      var AJ = new Dependent()
      {
        Id = Guid.NewGuid(),
        EmployeeId = Tony.Id,
        FirstName = "Anthony Jr.",
        LastName = "Soprano"
      };
      var Meadow = new Dependent()
      {
        Id = Guid.NewGuid(),
        EmployeeId = Tony.Id,
        FirstName = "Meadow",
        LastName = "Soprano"
      };
      var Carmela = new Dependent()
      {
        Id = Guid.NewGuid(),
        EmployeeId = Tony.Id,
        FirstName = "Carmela",
        LastName = "Soprano"
      };
      var enrollment = new Enrollment()
      {
        Id = Guid.NewGuid(),
        EmployeeId = Tony.Id,
        Benefit = BenefitTypes.GenericBenefit.ToString()
      };
      var discount = new Discount()
      {
        Id = Guid.NewGuid(),
        Name = DiscountTypes.NameDiscount.ToString(),
        EnrollmentId = enrollment.Id
      };

      modelBuilder.Entity<Dependent>().HasData(AJ, Meadow, Carmela);
      modelBuilder.Entity<Employee>().HasData(Tony);
      modelBuilder.Entity<Discount>().HasData(discount);
      modelBuilder.Entity<Enrollment>().HasData(enrollment);

      return modelBuilder;
    }

    public static ModelBuilder AddMoltisantis(this ModelBuilder modelBuilder)
    {
      var Christopher = new Employee()
      {
        Id = Guid.NewGuid(),
        FirstName = "Christopher",
        LastName = "Moltisanti",
        Salary = 52_000m,
        StartDate = DateTime.Now.Subtract(TimeSpan.FromDays(60))
      };
      var Kelli = new Dependent()
      {
        Id = Guid.NewGuid(),
        EmployeeId = Christopher.Id,
        FirstName = "Kelli",
        LastName = "Moltisanti"
      };
      var enrollment = new Enrollment()
      {
        Id = Guid.NewGuid(),
        EmployeeId = Christopher.Id,
        Benefit = BenefitTypes.GenericBenefit.ToString()
      };
      var discount = new Discount()
      {
        Id = Guid.NewGuid(),
        Name = DiscountTypes.NameDiscount.ToString(),
        EnrollmentId = enrollment.Id
      };
      
      modelBuilder.Entity<Dependent>().HasData(Kelli);
      modelBuilder.Entity<Employee>().HasData(Christopher);
      modelBuilder.Entity<Discount>().HasData(discount);
      modelBuilder.Entity<Enrollment>().HasData(enrollment);
      
      return modelBuilder;
    }
  }
}