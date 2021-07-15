using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BenefitsApp.Business.Entities.Benefits;
using BenefitsApp.Business.Entities.Discounts;

namespace BenefitsApp.Business.Requests
{
  public class CreateEnrollmentRequest
  {
    /// <summary>
    /// The id of the employee to enroll.
    /// </summary>
    [Required]
    public Guid EmployeeId { get; set; }
    
    /// <summary>
    /// The benefit in which to enroll the employee.
    /// </summary>
    [Required]
    public BenefitTypes Benefit { get; set; }
    
    /// <summary>
    /// The discounts applied to this enrollment.
    /// </summary>
    public IEnumerable<DiscountTypes> Discounts { get; set; }
  }
}