using System.Collections.Generic;

namespace BenefitsApp.Business.Responses
{
  public class DiscountsResponse
  {
    public IEnumerable<DiscountDetails> Discounts { get; set; }
  }

  public class DiscountDetails
  {
    public string Type { get; set; }
    public string Description { get; set; }
  }
}