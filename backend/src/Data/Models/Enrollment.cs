using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenefitsApp.Data.Models
{
  [Table("Enrollments")]
  public class Enrollment
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid EmployeeId { get; set; }
    
    [Required]
    public string Benefit { get; set; }
    
    public virtual Employee Employee { get; set; }
    
    public virtual ICollection<Discount> Discounts { get; set; }
  }
}