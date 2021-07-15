using System.Collections.Generic;

namespace BenefitsApp.Business.Responses
{
  public class BenefitsResponse
  {
    public IEnumerable<BenefitDetails> Benefits { get; set; }
  }

  public class BenefitDetails
  {
    public string Type { get; set; }
    public string Description { get; set; }
  }
}