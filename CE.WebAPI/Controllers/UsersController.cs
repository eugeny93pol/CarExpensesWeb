using CE.DataAccess;
using CE.DataAccess.Constants;
using CE.Service;
using CE.WebAPI.Helpers;
using CE.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUserSettingsService _userSettingsService;

        public UsersController(IUserService userService, IRoleService roleService, IUserSettingsService userSettingsService)
        {
            _userService = userService;
            _roleService = roleService;
            _userSettingsService = userSettingsService;
        }

        //[Authorize(Roles = RolesConstants.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] string[] include)
        {
            var users = await _userService.GetAll(includeProperties: include);
            return users.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id, [FromQuery]string[] include)
        {
            if (!AuthHelper.IsHasAccess(User, id)) { return Forbid(); }

            var user = include.Length != 0 ? 
                await _userService.GetById(id, include) : 
                await _userService.GetById(id);

            return user == null ? NotFound() : user;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(RegisterRequest request)
        {
            var roleUser = await _roleService.FirstOrDefault(r => r.Name == RolesConstants.User);

            var user = await _userService.CreateUser(request.GetUser(), roleUser);

            if (user == null) { return BadRequest("User already exist"); }

            await _userSettingsService.Create(new UserSettings(user.Id));

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(long id, PutUserRequest request)
        {
            if (!AuthHelper.IsHasAccess(User, id)) { return Forbid(); }

            if (id != request.Id) { return BadRequest(); }

            var user = await _userService.GetAsNoTracking(u => u.Id == id);

            if (user == null) { return NotFound(); }

            user = request.GetUser(user);

            try
            {
                await _userService.Update(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (!AuthHelper.IsHasAccess(User, id)) { return Forbid(); }

            var user = await _userService.GetById(id);

            if (user == null) { return NotFound(); }

            await _userService.Remove(user);

            return NoContent();
        }
    }
}
