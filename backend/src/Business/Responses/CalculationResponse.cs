using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BenefitsApp.Business.Responses
{
  public class CalculationResponse
  {
    public EnrollmentsResponse.EnrollmentDetails Enrollment { get; set; }
    public CalculationDetails Calculations { get; set; }

    public class EnrollmentDetails
    {
      public Guid Id { get; set; }
      public EmployeeDetails Employee { get; set; }
      public string Benefit { get; set; }
      public IEnumerable<string> Discounts { get; set; }
      public decimal Cost { get; set; }

      public class EmployeeDetails
      {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? StartDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<Responses.EmployeeDetails.DependentDetails> Dependents { get; set; }

        public class DependentDetails
        {
          public string FirstName { get; set; }
          public string LastName { get; set; }
        }
      }
    }

    public class CalculationDetails
    {
      public decimal Total { get; set; }
      public decimal Subtotal { get; set; }
      public decimal Discounts { get; set; }
      public IEnumerable<LineItemDetails> LineItems { get; set; }

      public class LineItemDetails
      {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
      }
    }
  }
}