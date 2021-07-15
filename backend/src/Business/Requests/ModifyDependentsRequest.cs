using System.ComponentModel.DataAnnotations;

namespace BenefitsApp.Business.Requests
{
  public class ModifyDependentsRequest
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