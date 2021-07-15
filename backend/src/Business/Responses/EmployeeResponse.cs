using System;
using System.Collections.Generic;

namespace BenefitsApp.Business.Responses
{
  public class EmployeeResponse
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime StartDate { get; set; }
    public decimal CostOfBenefits { get; set; }

    public IEnumerable<EnrollmentSummary> Enrollments { get; set; }
    
    public IEnumerable<DependentDetails> Dependents { get; set; }
    

    public class DependentDetails
    {
      public Guid Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
    }
    

    public class EnrollmentSummary
    {
      public Guid Id { get; set; }
      public string Benefit { get; set; }
      public IEnumerable<string> Discounts { get; set; }
    }
  }
}