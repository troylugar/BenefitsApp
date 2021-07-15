using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenefitsApp.Business.Entities;
using BenefitsApp.Business.Exceptions;
using BenefitsApp.Business.Requests;
using BenefitsApp.Data;
using BenefitsApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BenefitsApp.Business.Managers
{
  public interface IEnrollmentManager
  {
    /// <summary>
    /// Gets all enrollments.
    /// </summary>
    /// <returns>All enrollments.</returns>
    Task<IEnumerable<Enrollment>> GetAllAsync();
    
    /// <summary>
    /// Creates an enrollment from the given request.
    /// </summary>
    /// <param name="request">The details from which to create the enrollment.</param>
    /// <returns>The resulting enrollment.</returns>
    Task<Enrollment> CreateAsync(CreateEnrollmentRequest request);
    
    /// <summary>
    /// Finds an enrollment by it's id.
    /// </summary>
    /// <param name="id">The id of the requested enrollment.</param>
    /// <returns>The found enrollment or null if no enrollment is found.</returns>
    Task<Enrollment> GetByIdAsync(Guid id);

    /// <summary>
    /// Modifies an enrollment from a given request.
    /// </summary>
    /// <param name="enrollmentId">The id of the enrollment to modify.</param>
    /// <param name="request">The details from which to modify the enrollment.</param>
    /// <returns>The modified enrollment</returns>
    Task<Enrollment> ModifyAsync(Guid enrollmentId, ModifyEnrollmentRequest request);
    
    /// <summary>
    /// Removes an enrollment by it's id.
    /// </summary>
    /// <param name="id">The id of the relevant enrollment.</param>
    /// <returns></returns>
    Task RemoveByIdAsync(Guid id);
    
    /// <summary>
    /// Gets the calculations for an enrollment.
    /// </summary>
    /// <param name="id">The id of the relevant enrollment.</param>
    /// <returns>Calculations of the found enrollment.</returns>
    Task<Calculations> GetCalculationByEnrollmentIdAsync(Guid id);
    
    /// <summary>
    /// Gets the total cost for a given enrollment
    /// </summary>
    /// <param name="e">Enrollment for which to get the total cost.</param>
    /// <returns>The total cost of the given enrollment.</returns>
    decimal GetCostForEnrollment(Enrollment e);
  }

  public class EnrollmentManager : IEnrollmentManager
  {
    private readonly ILogger<EnrollmentManager> _logger;
    private readonly DatabaseContext _db;
    private readonly IBenefitsManager _benefitsManager;
    private readonly IDiscountsManager _discountsManager;

    public EnrollmentManager(ILogger<EnrollmentManager> logger,
      DatabaseContext db,
      IBenefitsManager benefitsManager,
      IDiscountsManager discountsManager)
    {
      _logger = logger;
      _db = db;
      _benefitsManager = benefitsManager;
      _discountsManager = discountsManager;
    }

    public async Task<IEnumerable<Enrollment>> GetAllAsync()
    {
      var result = _db.Enrollments
        .Include(x => x.Employee)
        .ThenInclude(x => x.Dependents)
        .Include(x => x.Discounts)
        .AsEnumerable();
      return await Task.FromResult(result);
    }

    public async Task<Enrollment> GetByIdAsync(Guid id)
    {
      var result = await _db.Enrollments
        .Include(x => x.Employee)
        .ThenInclude(x => x.Dependents)
        .Include(x => x.Discounts)
        .SingleOrDefaultAsync(x => x.Id == id);

      return result;
    }

    public async Task RemoveByIdAsync(Guid id)
    {
      var enrollment = await _db.Enrollments.FindAsync(id);
      if (enrollment == null)
      {
        throw new NotFoundException("Enrollment not found.");
      }
      _db.Enrollments.Remove(enrollment);
      await _db.SaveChangesAsync();
    }

    public async Task<Calculations> GetCalculationByEnrollmentIdAsync(Guid id)
    {
      var enrollment = await GetByIdAsync(id);
      return GetCalculationsForEnrollment(enrollment);
    }

    public async Task<Enrollment> CreateAsync(CreateEnrollmentRequest request)
    {
      if (_db.Enrollments.Any(x =>
        x.Benefit == request.Benefit.ToString() &&
        x.EmployeeId == request.EmployeeId))
      {
        var msg = "Enrollment exists for the given benefit and employee.";
        throw new DuplicateFoundException(msg);
      }

      var enrollment = request.ToModel();
      await _db.Enrollments.AddAsync(enrollment);
      await _db.SaveChangesAsync();
      return await GetByIdAsync(enrollment.Id);
    }

    public async Task<Enrollment> ModifyAsync(Guid enrollmentId, ModifyEnrollmentRequest request)
    {
      var enrollment = await GetByIdAsync(enrollmentId);

      if (enrollment == null)
      {
        throw new NotFoundException("Enrollment not found.");
      }

      if (_db.Enrollments.Any(x =>
        x.Benefit == request.Benefit.ToString() &&
        x.EmployeeId == enrollmentId))
      {
        var msg = "Enrollment exists for the given benefit and employee.";
        throw new DuplicateFoundException(msg);
      }

      enrollment.Benefit = request.Benefit.ToString();

      // keep only the discounts that are in the request
      enrollment.Discounts = enrollment.Discounts.Where(x =>
        request.Discounts.Any(y => y.ToString() == x.Name))
        .ToList();

      // add only the discounts that are in the request, but not yet in the enrollment
      request.Discounts
        .Where(x => enrollment.Discounts.All(y => y.Name != x.ToString()))
        .Select(x => new Discount()
        {
          EnrollmentId = enrollmentId,
          Name = x.ToString()
        })
        .ToList()
        .ForEach(enrollment.Discounts.Add);
      
      _db.Enrollments.Update(enrollment);
      await _db.SaveChangesAsync();
      return await GetByIdAsync(enrollment.Id);
    }

    public decimal GetCostForEnrollment(Enrollment e)
    {
      var benefit = _benefitsManager.GetBenefitByType(e.Benefit);
      var discounts = e.Discounts.Select(x => _discountsManager.GetDiscountByType(x.Name));
      var people = new List<Person> { e.Employee };
      people.AddRange(e.Employee.Dependents);
      var totalCost = 0m;
      foreach (var person in people)
      {
        var adjustedCost = benefit.CalculateCost(person);
        foreach (var discount in discounts)
        {
          var discountAmount = discount.Apply(benefit, person, adjustedCost);
          adjustedCost += discountAmount;
        }

        totalCost += adjustedCost;
      }

      return totalCost;
    }

    private Calculations GetCalculationsForEnrollment(Enrollment e)
    {
      var result = new Calculations
      {
        Enrollment = e
      };
      var lineItems = new List<LineItem>();
      var benefit = _benefitsManager.GetBenefitByType(e.Benefit);
      var discounts = e.Discounts.Select(x => _discountsManager.GetDiscountByType(x.Name));
      var people = new List<Person> { e.Employee };
      people.AddRange(e.Employee.Dependents);

      foreach (var person in people)
      {
        var cost = benefit.CalculateCost(person);
        result.Subtotal += cost;
        lineItems.Add(new LineItem()
        {
          Name = benefit.Type.ToString(),
          Description = benefit.Description,
          Amount = cost
        });

        foreach (var discount in discounts)
        {
          var discountAmount = discount.Apply(benefit, person, cost);
          lineItems.Add(new LineItem()
          {
            Name = discount.Type.ToString(),
            Description = discount.Description,
            Amount = discountAmount
          });
          cost += discountAmount;
        }
      }

      result.LineItems = lineItems;
      result.Subtotal = lineItems.Where(x => x.Amount > 0).Sum(x => x.Amount);
      result.Discounts = lineItems.Where(x => x.Amount < 0).Sum(x => x.Amount);
      result.Total = result.Subtotal + result.Discounts;

      return result;
    }
  }
}