using System;
using System.Threading.Tasks;
using BenefitsApp.Business.Exceptions;
using BenefitsApp.Business.Managers;
using BenefitsApp.Business.Requests;
using BenefitsApp.Business.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BenefitsApp.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class EmployeesController : ControllerBase
  {
    private readonly ILogger<EmployeesController> _logger;
    private readonly IEmployeeManager _employeeManager;
    private readonly IEnrollmentManager _enrollmentManager;

    public EmployeesController(ILogger<EmployeesController> logger,
      IEmployeeManager employeeManager,
      IEnrollmentManager enrollmentManager)
    {
      _logger = logger;
      _employeeManager = employeeManager;
      _enrollmentManager = enrollmentManager;
    }

    [HttpGet]
    public async Task<ActionResult<EmployeesResponse>> Get()
    {
      var result = await _employeeManager.GetEmployeesAsync();
      return Ok(result.ToResponse());
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeResponse>> Post(CreateEmployeeRequest request)
    {
      var result = await _employeeManager.CreateEmployee(request);
      return CreatedAtAction(
        nameof(GetById),
        new { employeeId = result.Id },
        result.ToResponse());
    }

    [HttpGet("{employeeId:guid}")]
    [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeResponse>> GetById(Guid employeeId)
    {
      var result = await _employeeManager.GetEmployeeById(employeeId);
      if (result == null) return NotFound();
      var response = result.ToResponse();
      return Ok(response);
    }

    [HttpPut("{employeeId:guid}")]
    [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeResponse>> ModifyById(Guid employeeId, ModifyEmployeeRequest request)
    {
      try
      {
        var result = await _employeeManager.ModifyEmployee(employeeId, request);
        return Ok(result.ToResponse());
      }
      catch (NotFoundException e)
      {
        return NotFound();
      }
    }

    [HttpDelete("{employeeId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeResponse>> DeleteById(Guid employeeId)
    {
      try
      {
        await _employeeManager.DeleteEmployee(employeeId);
        return NoContent();
      }
      catch (NotFoundException e)
      {
        return NotFound(e.Message);
      }
    }

    [HttpPost("{employeeId:guid}/Dependents")]
    [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeResponse>> AddDependents(Guid employeeId, CreateDependentsRequest request)
    {
      try
      {
        var result = await _employeeManager.CreateDependents(employeeId, request);
        return Ok(result.ToResponse());
      }
      catch (NotFoundException e)
      {
        return NotFound(e.Message);
      }
    }

    [HttpGet("{employeeId:guid}/Dependents/{dependentId:guid}")]
    [ProducesResponseType(typeof(DependentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DependentResponse>> GetDependent(Guid employeeId, Guid dependentId)
    {
      try
      {
        var result = await _employeeManager.GetDependentById(employeeId, dependentId);
        return Ok(result.ToResponse());
      }
      catch (NotFoundException e)
      {
        return NotFound(e.Message);
      }
    }

    [HttpPut("{employeeId:guid}/Dependents/{dependentId:guid}")]
    [ProducesResponseType(typeof(DependentResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DependentResponse>> ModifyDependent(Guid employeeId, Guid dependentId, ModifyDependentsRequest request)
    {
      try
      {
        var result = await _employeeManager.ModifyDependent(dependentId, request);
        return AcceptedAtAction(
          nameof(GetById),
          new { employeeId },
          result.ToResponse());
      }
      catch (NotFoundException e)
      {
        return NotFound(e.Message);
      }
    }

    [HttpDelete("{employeeId:guid}/Dependents/{dependentId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeResponse>> DeleteDependent(Guid employeeId, Guid dependentId)
    {
      try
      {
        await _employeeManager.DeleteDependent(dependentId);
        return NoContent();
      }
      catch (NotFoundException e)
      {
        return NotFound(e.Message);
      }
    }
  }
}