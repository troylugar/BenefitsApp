using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenefitsApp.Business.Exceptions;
using BenefitsApp.Business.Requests;
using BenefitsApp.Data;
using BenefitsApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BenefitsApp.Business.Managers
{
  public interface IEmployeeManager
  {
    /// <summary>
    /// Gets a list of all employees.
    /// </summary>
    /// <returns>A list of all employees</returns>
    Task<IEnumerable<Employee>> GetEmployeesAsync();
    
    /// <summary>
    /// Find an employee based on their id.
    /// </summary>
    /// <param name="id">the id of the employee to find</param>
    /// <param name="includeDependents">whether or not dependents should be returned</param>
    /// <param name="includeEnrollments">whether or not enrollments should be returned</param>
    /// <returns>An employee with the given id or null</returns>
    Task<Employee> GetEmployeeById(Guid id, bool includeDependents = true, bool includeEnrollments = true);
    
    /// <summary>
    /// Creates an employee from a given request.
    /// </summary>
    /// <param name="request">The details from which to create the employee.</param>
    /// <returns>The resulting employee</returns>
    Task<Employee> CreateEmployee(CreateEmployeeRequest request);

    /// <summary>
    /// Modifies an employee from a given request.
    /// </summary>
    /// <param name="request">The details from which to modify the employee.</param>
    /// <returns>The modified employee</returns>
    Task<Employee> ModifyEmployee(Guid employeeId, ModifyEmployeeRequest request);
    
    /// <summary>
    /// Deletes the employee with the given id.
    /// </summary>
    /// <param name="employeeId">The id of the employee to delete.</param>
    /// <returns></returns>
    Task DeleteEmployee(Guid employeeId);

    /// <summary>
    /// Gets a dependent matching the given ids.
    /// </summary>
    /// <param name="employeeId">The id of the employee to which the dependent belongs.</param>
    /// <param name="dependentId">The id of the dependent.</param>
    /// <returns>The dependent with the given ids.</returns>
    Task<Dependent> GetDependentById(Guid employeeId, Guid dependentId);
    
    /// <summary>
    /// Adds dependents to an existing employee.
    /// </summary>
    /// <param name="employeeId">The id of the employee receiving a new dependent.</param>
    /// <param name="request">The details from which to create the dependents. </param>
    /// <returns>The employee with their new dependents.</returns>
    Task<Employee> CreateDependents(Guid employeeId, CreateDependentsRequest request);
    
    /// <summary>
    /// Modifies a dependent from a given request.
    /// </summary>
    /// <param name="dependentId">The id of the dependent to modify.</param>
    /// <param name="request">The details from which to modify the dependent. </param>
    /// <returns>The modified dependent.</returns>
    Task<Dependent> ModifyDependent(Guid dependentId, ModifyDependentsRequest request);
    
    /// <summary>
    /// Deletes the dependent with the given id.
    /// </summary>
    /// <param name="dependentId">The id of the dependent to delete.</param>
    /// <returns></returns>
    Task DeleteDependent(Guid dependentId);
  }

  public class EmployeeManager : IEmployeeManager
  {
    private readonly ILogger<EmployeeManager> _logger;
    private readonly DatabaseContext _db;
    private readonly IEnrollmentManager _enrollmentManager;

    public EmployeeManager(ILogger<EmployeeManager> logger, DatabaseContext db, IEnrollmentManager enrollmentManager)
    {
      _logger = logger;
      _db = db;
      _enrollmentManager = enrollmentManager;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
      var result = _db.Employees.AsEnumerable();
      return await Task.FromResult(result);
    }

    public async Task<Employee> GetEmployeeById(Guid id, bool includeDependents = true, bool includeEnrollments = true)
    {
      var repository = _db.Employees.AsQueryable();
      if (includeDependents)
      {
        repository = repository.Include(x => x.Dependents);
      }

      if (includeEnrollments)
      {
        repository = repository
          .Include(x => x.Enrollments)
          .ThenInclude(x => x.Discounts);
      }
      
      var result = await repository.SingleOrDefaultAsync(x => x.Id == id);
      if (includeEnrollments)
      {
        result.CostOfBenefits = result.Enrollments.Sum(_enrollmentManager.GetCostForEnrollment);
      }
      return result;
    }

    public async Task<Employee> CreateEmployee(CreateEmployeeRequest request)
    {
      var model = request.ToModel();
      _db.Employees.Add(model);
      await _db.SaveChangesAsync();
      return model;
    }

    public async Task<Employee> ModifyEmployee(Guid employeeId, ModifyEmployeeRequest request)
    {
      var employee = await GetEmployeeById(employeeId);
      if (employee == null)
      {
        throw new NotFoundException("Employee not found.");
      }
      employee.FirstName = request.FirstName;
      employee.LastName = request.LastName;
      employee.StartDate = request.StartDate.GetValueOrDefault();
      employee.Salary = request.Salary.GetValueOrDefault();
      _db.Employees.Update(employee);
      await _db.SaveChangesAsync();
      return employee;
    }
    
    public async Task<Dependent> GetDependentById(Guid employeeId, Guid dependentId)
    {
      return await _db.Dependents.SingleOrDefaultAsync(x =>
        x.EmployeeId == employeeId &&
        x.Id == dependentId);
    }
    
    public async Task<Employee> CreateDependents(Guid employeeId, CreateDependentsRequest request)
    {
      var employee = await GetEmployeeById(employeeId);
      if (employee == null)
      {
        throw new NotFoundException("Employee not found.");
      }

      var dependents = request.ToModel();
      foreach (var dependent in dependents)
      {
        dependent.EmployeeId = employeeId;
        employee.Dependents.Add(dependent);
      }

      await _db.SaveChangesAsync();
      return await GetEmployeeById(employeeId);
    }

    public async Task<Dependent> ModifyDependent(Guid dependentId, ModifyDependentsRequest request)
    {
      var dependent = await _db.Dependents.FindAsync(dependentId);
      if (dependent == null)
      {
        throw new NotFoundException("Dependent not found.");
      }
      dependent.FirstName = request.FirstName;
      dependent.LastName = request.LastName;
      _db.Dependents.Update(dependent);
      await _db.SaveChangesAsync();
      return dependent;
    }

    public async Task DeleteEmployee(Guid employeeId)
    {
      var employee = await _db.Employees.FindAsync(employeeId);
      if (employee == null)
      {
        throw new NotFoundException("Employee not found.");
      }
      
      _db.Employees.Remove(employee);
      await _db.SaveChangesAsync();
    }

    public async Task DeleteDependent(Guid dependentId)
    {
      var dependent = await _db.Dependents.FindAsync(dependentId);
      if (dependent == null)
      {
        throw new NotFoundException("Dependent not found.");
      }
      
      _db.Dependents.Remove(dependent);
      await _db.SaveChangesAsync();
    }
  }
}