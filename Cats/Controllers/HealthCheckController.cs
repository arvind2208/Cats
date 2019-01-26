using Microsoft.AspNetCore.Mvc;

namespace Cats.Controllers
{
    public class HealthCheckController : Controller
    {
        [Route("api/health"), HttpGet]
        public IActionResult CheckHealth()
        {
            return Ok("alive");
        }
    }
}
