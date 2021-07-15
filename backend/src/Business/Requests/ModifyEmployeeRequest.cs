using System;
using System.ComponentModel.DataAnnotations;

namespace BenefitsApp.Business.Requests
{
  public class ModifyEmployeeRequest
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
    /// The date the employee starts their employement.
    /// </summary>
    [Required]
    public DateTime? StartDate { get; set; }
    
    [Required]
    public decimal? Salary { get; set; }
  }
}