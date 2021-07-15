using System;
using System.Linq;
using System.Threading.Tasks;
using BenefitsApp.Business.Exceptions;
using BenefitsApp.Business.Managers;
using BenefitsApp.Business.Requests;
using BenefitsApp.Business.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;

namespace BenefitsApp.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class EnrollmentsController : ControllerBase
  {
    private readonly ILogger<EnrollmentsController> _logger;
    private readonly IEnrollmentManager _enrollmentManager;

    public EnrollmentsController(ILogger<EnrollmentsController> logger, IEnrollmentManager enrollmentManager)
    {
      _logger = logger;
      _enrollmentManager = enrollmentManager;
    }

    [HttpGet]
    public async Task<ActionResult<EnrollmentsResponse>> Get()
    {
      var result = await _enrollmentManager.GetAllAsync();
      var response = result.ToResponse(_enrollmentManager.GetCostForEnrollment);
      response.Cost = response.Enrollments.Sum(x => x.Cost);
      return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EnrollmentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EnrollmentResponse>> Post(CreateEnrollmentRequest request)
    {
      try
      {
        var result = await _enrollmentManager.CreateAsync(request);
        return CreatedAtAction(
          nameof(GetById),
          new { enrollmentId = result.Id },
          result.ToResponse(_enrollmentManager.GetCostForEnrollment));
      }
      catch (DuplicateFoundException e)
      {
        return BadRequest(e.Message);
      }
    }
    
    [HttpGet("{enrollmentId:guid}")]
    [ProducesResponseType(typeof(EnrollmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EnrollmentResponse>> GetById(Guid enrollmentId)
    {
      var result = await _enrollmentManager.GetByIdAsync(enrollmentId);
      if (result == null)
      {
        return NotFound();
      }
      var response = result.ToResponse(_enrollmentManager.GetCostForEnrollment);
      response.Cost = _enrollmentManager.GetCostForEnrollment(result);
      return Ok(response);
    }

    [HttpPut("{enrollmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ModifyById(Guid enrollmentId, ModifyEnrollmentRequest request)
    {
      try
      {
        var enrollment = await _enrollmentManager.ModifyAsync(enrollmentId, request);
        var response = enrollment.ToResponse(_enrollmentManager.GetCostForEnrollment);
        return AcceptedAtAction(
          nameof(GetById),
          new { enrollmentId },
          response);
      }
      catch (NotFoundException e)
      {
        return NotFound(e.Message);
      }
      catch (DuplicateFoundException e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{enrollmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteById(Guid enrollmentId)
    {
      try
      {
        await _enrollmentManager.RemoveByIdAsync(enrollmentId);
      }
      catch (NotFoundException e)
      {
        return NotFound(e.Message);
      }

      return NoContent();
    }

    [HttpGet("{enrollmentId:guid}/Calculations")]
    [ProducesResponseType(typeof(CalculationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CalculationResponse>> GetCalculationById(Guid enrollmentId)
    {
      try
      {
        var result = await _enrollmentManager.GetCalculationByEnrollmentIdAsync(enrollmentId);
        return Ok(result.ToResponse());
      }
      catch (NotFoundException e)
      {
        return NotFound(e.Message);
      }
    }
  }
}