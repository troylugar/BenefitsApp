using BenefitsApp.Data.Models;

namespace BenefitsApp.Business.Entities.Benefits
{
  public interface IBenefit
  {
    /// <summary>
    /// The type of this benefit.
    /// </summary>
    BenefitTypes Type { get; }
    
    /// <summary>
    /// The description of this benefit.
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Calculates the cost of this benefit for a given beneficiary.
    /// </summary>
    /// <param name="beneficiary">the beneficiary receiving this benefit</param>
    /// <returns>the cost of this benefit</returns>
    decimal CalculateCost(Person beneficiary);
  }
}