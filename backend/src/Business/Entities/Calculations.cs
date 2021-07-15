using System.Collections.Generic;
using BenefitsApp.Data.Models;

namespace BenefitsApp.Business.Entities
{
  public class Calculations
  {
    /// <summary>
    /// Total costs after discounts.
    /// </summary>
    public decimal Total { get; set; }
    
    /// <summary>
    /// Total costs before discounts
    /// </summary>
    public decimal Subtotal { get; set; }
    
    /// <summary>
    /// Total discounts
    /// </summary>
    public decimal Discounts { get; set; }
    
    /// <summary>
    /// Enrollment used for this calculation.
    /// </summary>
    public Enrollment Enrollment { get; set; }
    
    /// <summary>
    /// Costs and deductions for benefits and discounts.
    /// </summary>
    public IEnumerable<LineItem> LineItems { get; set; }
  }

  public class LineItem
  {
    /// <summary>
    /// The name of the item.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// A description of the item.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// The total amount of the charge (positive or negative).
    /// </summary>
    public decimal Amount { get; set; }
  }
}