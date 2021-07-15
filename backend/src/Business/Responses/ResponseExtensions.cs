using System;
using System.Collections.Generic;
using System.Linq;
using BenefitsApp.Business.Entities;
using BenefitsApp.Business.Entities.Benefits;
using BenefitsApp.Business.Entities.Discounts;
using BenefitsApp.Data.Models;

namespace BenefitsApp.Business.Responses
{
  public static class ResponseExtensions
  {
    public static EmployeesResponse ToResponse(this IEnumerable<Employee> employees)
    {
      return new EmployeesResponse()
      {
        Employees = employees.Select(x => new EmployeeDetails()
        {
          Id = x.Id,
          FirstName = x.FirstName,
          LastName = x.LastName,
          Salary = x.Salary.GetValueOrDefault(),
          StartDate = x.StartDate.GetValueOrDefault()
        })
      };
    }

    public static EmployeeResponse ToResponse(this Employee employee)
    {
      return new EmployeeResponse()
      {
        Id = employee.Id,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        CostOfBenefits = employee.CostOfBenefits,
        Salary = employee.Salary.GetValueOrDefault(),
        StartDate = employee.StartDate.GetValueOrDefault(),
        Dependents = employee.Dependents?.Select(x => new EmployeeResponse.DependentDetails()
        {
          Id = x.Id,
          FirstName = x.FirstName,
          LastName = x.LastName
        }),
        Enrollments = employee.Enrollments?.Select(x => new EmployeeResponse.EnrollmentSummary()
        {
          Id = x.Id,
          Benefit = x.Benefit,
          Discounts = x.Discounts.Select(y => y.Name)
        })
      };
    }

    public static BenefitsResponse ToResponse(this IEnumerable<IBenefit> benefits)
    {
      return new BenefitsResponse()
      {
        Benefits = benefits.Select(x => new BenefitDetails()
        {
            Type = x.Type.ToString(),
            Description = x.Description
          })
      };
    }

    public static DiscountsResponse ToResponse(this IEnumerable<IDiscount> discounts)
    {
      return new DiscountsResponse()
      {
        Discounts = discounts.Select(x => new DiscountDetails()
        {
          Type = x.Type.ToString(),
          Description = x.Description
        })
      };
    }

    public static EnrollmentsResponse ToResponse(this IEnumerable<Enrollment> enrollments, Func<Enrollment, decimal> GetCostForEnrollment)
    {
      return new EnrollmentsResponse()
      {
        Enrollments = enrollments.Select(x => new EnrollmentsResponse.EnrollmentDetails()
        {
          Id = x.Id,
          Employee = new EnrollmentsResponse.EnrollmentDetails.EmployeeDetails()
          {
            Id = x.EmployeeId,
            FirstName = x.Employee?.FirstName,
            LastName = x.Employee?.LastName,
            StartDate = x.Employee?.StartDate,
            Salary = x.Employee?.Salary,
          },
          Benefit = x.Benefit,
          Discounts = x.Discounts?.Select(y => y.Name),
          Cost = GetCostForEnrollment(x)
        })
      };
    }

    public static EnrollmentResponse ToResponse(this Enrollment enrollment, Func<Enrollment, decimal> GetCostForEnrollment)
    {
      return new EnrollmentResponse()
      {
        Id = enrollment.Id,
        Employee = new EmployeeDetails()
        {
          Id = enrollment.EmployeeId,
          FirstName = enrollment.Employee?.FirstName,
          LastName = enrollment.Employee?.LastName
        },
        Benefit = enrollment.Benefit,
        Discounts = enrollment.Discounts?.Select(x => x.Name),
        Cost = GetCostForEnrollment(enrollment)
      };
    }

    public static CalculationResponse ToResponse(this Calculations calculations)
    {
      return new CalculationResponse()
      {
        Calculations = new CalculationResponse.CalculationDetails()
        {
          Discounts = calculations.Discounts,
          LineItems = calculations.LineItems.Select(x => new CalculationResponse.CalculationDetails.LineItemDetails()
          {
            Amount = x.Amount,
            Description = x.Description,
            Name = x.Name
          }),
          Subtotal = calculations.Subtotal,
          Total = calculations.Total
        },
        Enrollment = new EnrollmentsResponse.EnrollmentDetails()
        {
          Id = calculations.Enrollment.Id,
          Benefit = calculations.Enrollment.Benefit,
          Discounts = calculations.Enrollment.Discounts.Select(x => x.Name),
          Employee = new EnrollmentsResponse.EnrollmentDetails.EmployeeDetails()
          {
            Id = calculations.Enrollment.Employee.Id,
            FirstName = calculations.Enrollment.Employee.FirstName,
            LastName = calculations.Enrollment.Employee.LastName,
            Dependents = calculations.Enrollment.Employee.Dependents.Select(x => new EmployeeDetails.DependentDetails()
            {
              FirstName = x.FirstName,
              LastName = x.LastName
            })
          },
          Cost = calculations.Total
        }
      };
    }

    public static DependentResponse ToResponse(this Dependent dependent)
    {
      return new DependentResponse()
      {
        Id = dependent.Id,
        FirstName = dependent.FirstName,
        LastName = dependent.LastName
      };
    }
  }
}