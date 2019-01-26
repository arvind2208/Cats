using Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Cats.Controllers
{
    
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IService<GetCatsByOwnersGenderRequest, GetCatsByOwnersGenderResponse> _service;

        public CatsController(ILoggerFactory loggerFactory, IService<GetCatsByOwnersGenderRequest, GetCatsByOwnersGenderResponse> service)
        {
            _logger = loggerFactory.CreateLogger<CatsController>();
            _service = service;
        }

        [Route("api/cats"), HttpGet]
        public IActionResult GetCats([FromQuery]string ownersGender = null)
        {
            _logger.LogTrace($"gender searched for: {ownersGender}");

            var response = _service.Invoke(new GetCatsByOwnersGenderRequest
            {
                OwnerGender = ownersGender
            });

            return Ok(response);
        }
    }
}