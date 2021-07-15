using System;
using BenefitsApp.Business.Entities.Benefits;
using BenefitsApp.Data.Models;

namespace BenefitsApp.Business.Entities.Discounts
{
  public class NameDiscount : IDiscount
  {
    public DiscountTypes Type => DiscountTypes.NameDiscount;
    public string Description => "10% Discount for anyone with a name that starts with an 'A'";
    public decimal Apply(IBenefit benefit, Person beneficiary, decimal adjustedCost)
    {
      return beneficiary.FirstName.StartsWith("A", StringComparison.InvariantCultureIgnoreCase)
        ? -0.10m * benefit.CalculateCost(beneficiary)
        : 0m;
    }
  }
}