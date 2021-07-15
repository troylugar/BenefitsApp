using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BenefitsApp.Business.Responses
{
  public class EnrollmentsResponse
  {
    public IEnumerable<EnrollmentDetails> Enrollments { get; set; }
    public decimal Cost { get; set; }

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
  }
}