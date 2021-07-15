using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BenefitsApp.Business.Requests
{
  public class CreateEmployeeRequest
  {
    /// <summary>
    /// The employee's first/given name.
    /// </summary>
    [Required]
    public string FirstName { get; set; }
    
    /// <summary>
    /// The employee's last/family name.
    /// </summary>
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// The employee's start date.
    /// </summary>
    [Required]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// The employee's dependents.
    /// </summary>
    public IEnumerable<DependentDetails> Dependents { get; set; }

    public class DependentDetails
    {
      /// <summary>
      /// The dependent's first/given name.
      /// </summary>
      [Required]
      public string FirstName { get; set; }
    
      /// <summary>
      /// The dependent's last/family name.
      /// </summary>
      [Required]
      public string LastName { get; set; }
    }
  }
}