using System.ComponentModel.DataAnnotations;

namespace BenefitsApp.Data.Models
{
  public abstract class Person
  {
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
  }
}