using System.Collections.Generic;
using System.Linq;
using BenefitsApp.Business.Entities.Discounts;
using BenefitsApp.Data;
using Microsoft.Extensions.Logging;

namespace BenefitsApp.Business.Managers
{
  public interface IDiscountsManager
  {
    /// <summary>
    /// Gets all available discounts.
    /// </summary>
    /// <returns>All available discounts.</returns>
    IEnumerable<IDiscount> GetDiscounts();
    
    /// <summary>
    /// Returns a discount implementation given a type of discount.
    /// </summary>
    /// <param name="type"></param>
    /// <returns>An discount implementation matching the type given</returns>
    IDiscount GetDiscountByType(string type);
  }

  public class DiscountsManager : IDiscountsManager
  {
    private readonly ILogger<DiscountsManager> _logger;
    private readonly IEnumerable<IDiscount> _discounts;
    private readonly DatabaseContext _db;

    public DiscountsManager(ILogger<DiscountsManager> logger, IEnumerable<IDiscount> discounts, DatabaseContext db)
    {
      _logger = logger;
      _discounts = discounts;
      _db = db;
    }

    public IEnumerable<IDiscount> GetDiscounts()
    {
      return _discounts;
    }

    public IDiscount GetDiscountByType(string type)
    {
      return _discounts.Single(x => x.Type.ToString() == type);
    }
  }
}