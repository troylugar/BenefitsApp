using BenefitsApp.Business.Entities.Benefits;
using BenefitsApp.Data.Models;

namespace BenefitsApp.Business.Entities.Discounts
{
  public interface IDiscount
  {
    /// <summary>
    /// The type of this discount.
    /// </summary>
    DiscountTypes Type { get; }
   
    /// <summary>
    /// The description of this discount.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Applies the discount against the cost of the given benefit for the given
    /// beneficiary.
    /// </summary>
    /// <param name="benefit">the type of benefit this discount applies to</param>
    /// <param name="beneficiary">the given beneficiary</param>
    /// <param name="adjustedCost">the cost to apply the discount towards</param>
    /// <returns>The amount discounted.</returns>
    decimal Apply(IBenefit benefit, Person beneficiary, decimal adjustedCost);
  }
}