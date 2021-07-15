using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenefitsApp.Data.Models
{
  [Table("Dependents")]
  public class Dependent : Person
  {
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid EmployeeId { get; set; }
    
    public virtual Employee Employee { get; set; }
  }
}