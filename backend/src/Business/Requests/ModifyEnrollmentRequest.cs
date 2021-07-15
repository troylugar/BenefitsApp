using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BenefitsApp.Business.Entities.Benefits;
using BenefitsApp.Business.Entities.Discounts;

namespace BenefitsApp.Business.Requests
{
  public class ModifyEnrollmentRequest
  {
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