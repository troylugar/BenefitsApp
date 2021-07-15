using BenefitsApp.Business.Managers;
using BenefitsApp.Business.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BenefitsApp.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class BenefitsController : ControllerBase
  {
    private readonly ILogger<BenefitsController> _logger;
    private readonly IBenefitsManager _benefitsManager;

    public BenefitsController(ILogger<BenefitsController> logger, IBenefitsManager benefitsManager)
    {
      _logger = logger;
      _benefitsManager = benefitsManager;
    }

    [HttpGet]
    public ActionResult<BenefitsResponse> Get()
    {
      var result = _benefitsManager.GetBenefits();
      return Ok(result.ToResponse());
    }
  }
}