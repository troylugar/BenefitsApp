using System;
using System.Collections.Generic;
using System.Linq;
using BenefitsApp.Data.Models;

namespace BenefitsApp.Business.Requests
{
  public static class RequestExtensions
  {
    public static Employee ToModel(this CreateEmployeeRequest request)
    {
      var result = new Employee()
      {
        FirstName = request.FirstName,
        LastName = request.LastName,
        StartDate = request.StartDate
      };
      if (request.Dependents == null) return result;
      
      result.Dependents = request.Dependents.Select(dependent => new Dependent()
      {
        FirstName = dependent.FirstName,
        LastName = dependent.LastName
      }).ToList();

      return result;
    }

    public static IEnumerable<Dependent> ToModel(this CreateDependentsRequest request)
    {
      return request.Dependents.Select(d => new Dependent()
      {
        FirstName = d.FirstName,
        LastName = d.LastName
      });
    }

    public static Enrollment ToModel(this CreateEnrollmentRequest request)
    {
      return new Enrollment()
      {
        Benefit = request.Benefit.ToString(),
        Discounts = request.Discounts.Select(x => new Discount()
        {
          Name = x.ToString()
        }).ToList(),
        EmployeeId = request.EmployeeId
      };
    }
  }
}