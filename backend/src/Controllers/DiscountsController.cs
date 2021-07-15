using BenefitsApp.Business.Managers;
using BenefitsApp.Business.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BenefitsApp.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class DiscountsController : ControllerBase
  {
    private readonly ILogger<DiscountsController> _logger;
    private readonly IDiscountsManager _discountsManager;

    public DiscountsController(ILogger<DiscountsController> logger, IDiscountsManager discountsManager)
    {
      _logger = logger;
      _discountsManager = discountsManager;
    }

    [HttpGet]
    public ActionResult<DiscountsResponse> Get()
    {
      var result = _discountsManager.GetDiscounts();
      return Ok(result.ToResponse());
    }
  }
}