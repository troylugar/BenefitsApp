using System;
using System.Collections.Generic;

namespace BenefitsApp.Business.Responses
{
  public class EnrollmentResponse
  {
    public Guid Id { get; set; }
    public EmployeeDetails Employee { get; set; }
    public string Benefit { get; set; }
    public IEnumerable<string> Discounts { get; set; }
    public decimal Cost { get; set; }
  }
}