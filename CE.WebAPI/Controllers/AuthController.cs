using System;
using CE.WebAPI.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using CE.Service.Interfaces;
using CE.WebAPI.RequestModels;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthOptions _authOptions;

        public AuthController(IUserService userService, IOptions<AuthOptions> authOptions)
        {
            _userService = userService;
            _authOptions = authOptions.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userService.Authenticate(request.Email, request.Password);
            if (user == null)
                return Unauthorized();
            var accessToken = _authOptions.GenerateToken(user);
            return Ok(new { accessToken });
        }

        [Authorize]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
