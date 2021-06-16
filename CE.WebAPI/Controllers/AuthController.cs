using System;
using CE.WebAPI.Helpers;
using CE.WebAPI.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using CE.Service.Interfaces;
using CE.WebAPI.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthOptions _authOptions;
        private readonly ILogger<UsersController> _logger;

        public AuthController(
            IUserService userService, 
            IOptions<AuthOptions> authOptions, 
            ILogger<UsersController> logger)
        {
            _userService = userService;
            _authOptions = authOptions.Value;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var user = await _userService.Authenticate(request.Email, request.Password);
                if (user == null)
                    return Unauthorized();
                var accessToken = AuthHelper.GenerateToken(user, _authOptions);
                return Ok(new { accessToken });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
