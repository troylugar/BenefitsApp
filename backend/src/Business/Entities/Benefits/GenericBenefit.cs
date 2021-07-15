using BenefitsApp.Data.Models;

namespace BenefitsApp.Business.Entities.Benefits
{
  public class GenericBenefit : IBenefit
  {
    public BenefitTypes Type => BenefitTypes.GenericBenefit;
    public string Description => "A generic benefit.";
    public decimal CalculateCost(Person beneficiary)
    {
      return beneficiary is Employee ? 1000 : 500;
    }
  }
}