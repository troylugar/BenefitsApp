using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenefitsApp.Data.Models
{
  [Table("Employees")]
  public class Employee : Person
  {
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public decimal? Salary { get; set; }
    
    [Required]
    public DateTime? StartDate { get; set; }

    [NotMapped]
    public decimal CostOfBenefits { get; set; }
    
    public ICollection<Dependent> Dependents { get; set; }
    
    public ICollection<Enrollment> Enrollments { get; set; }
  }
}