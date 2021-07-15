using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenefitsApp.Data.Models
{
  [Table("Discounts")]
  public class Discount
  {
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public Guid EnrollmentId { get; set; }
    
    public virtual Enrollment Enrollment { get; set; }
  }
}