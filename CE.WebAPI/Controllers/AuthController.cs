﻿using CE.Service;
using CE.WebAPI.Helpers;
using CE.WebAPI.Models;
using CE.WebAPI.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthOptions _authOptions;

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
            {
                return Unauthorized();
            }

            var jwt = AuthHelper.GenerateToken(user, _authOptions);

            return Ok(new { access_token = jwt });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            long UserID = AuthHelper.GetUserID(User);

            return Ok(await _userService.GetById(UserID));
        }
    }
}