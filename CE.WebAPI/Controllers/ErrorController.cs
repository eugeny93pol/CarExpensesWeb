using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CE.WebAPI.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public ErrorController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
