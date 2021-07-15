using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BenefitsApp.Business.Responses
{
  public class EmployeesResponse
  {
    public IEnumerable<EmployeeDetails> Employees { get; set; }
  }

  public class EmployeeDetails
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime StartDate { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IEnumerable<DependentDetails> Dependents { get; set; }
    
    public class DependentDetails
    {
      public string FirstName { get; set; }
      public string LastName { get; set; }
    }
  }
}