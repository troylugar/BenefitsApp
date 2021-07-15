using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BenefitsApp.Business.Requests
{
  public class CreateDependentsRequest
  {
    /// <summary>
    /// The dependents to be created.
    /// </summary>
    [Required]
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