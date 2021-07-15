using System.Collections.Generic;
using System.Linq;
using BenefitsApp.Business.Entities.Benefits;
using BenefitsApp.Data;
using Microsoft.Extensions.Logging;

namespace BenefitsApp.Business.Managers
{
  public interface IBenefitsManager
  {
    /// <summary>
    /// Get a list of all available benefits.
    /// </summary>
    /// <returns>all available benefits</returns>
    IEnumerable<IBenefit> GetBenefits();
    
    /// <summary>
    /// Returns a benefit implementation given a type of benefit.
    /// </summary>
    /// <param name="type">The type of benefit to return.</param>
    /// <returns>A benefit implementation matching the type given.</returns>
    IBenefit GetBenefitByType(string type);
  }

  public class BenefitsManager : IBenefitsManager
  {
    private readonly ILogger<BenefitsManager> _logger;
    private readonly IEnumerable<IBenefit> _benefits;
    private readonly DatabaseContext _db;

    public BenefitsManager(ILogger<BenefitsManager> logger, IEnumerable<IBenefit> benefits, DatabaseContext db)
    {
      _logger = logger;
      _benefits = benefits;
      _db = db;
    }

    public IEnumerable<IBenefit> GetBenefits()
    {
      return _benefits;
    }

    public IBenefit GetBenefitByType(string type)
    {
      return _benefits.Single(x => x.Type.ToString() == type);
    }
  }
}