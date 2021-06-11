using CE.DataAccess;
using CE.DataAccess.Constants;
using CE.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.Service.Interfaces;
using CE.WebAPI.RequestModels;

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

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAll();
            return users.ToList();
        }

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpGet("info")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersFullInfo()
        {
            var users = await _userService.GetAll(u => u.Cars, u => u.Settings);
            return users.ToList();
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            if (!AuthHelper.IsHasAccess(User, id))
                return Forbid();

            var user = await _userService.GetById(id);

            return user != null ? Ok(user) : NotFound();
        }

        [HttpGet("{id:Guid}/info")]
        public async Task<ActionResult<User>> GetUserFullInfo(Guid id)
        {
            if (!AuthHelper.IsHasAccess(User, id))
                return Forbid();

            var user = await _userService.GetById(id, u => u.Cars, u => u.Settings);

            return user != null ? Ok(user) : NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(RegisterRequest request)
        {
            var roleUser = await _roleService.FirstOrDefault(r => r.Name == RolesConstants.User);

            var user = await _userService.CreateUser(request.GetUser(), roleUser);

            if (user == null) 
                return BadRequest("User already exist");

            await _userSettingsService.Create(new UserSettings(user.Id));

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> EditUser(Guid id, PutUser request)
        {
            var user = await _userService.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            request.UpdateUser(user);
            user.Password = _userService.GeneratePasswordHash(request.Password);

            try
            {
                await _userService.Update(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, PatchUser user)
        {
            if (!AuthHelper.IsHasAccess(User, id))
                return Forbid();

            var saved = await _userService.GetById(id);

            if (saved == null)
                return NotFound();

            try
            {
                await _userService.UpdatePartial(saved, user.GetUser());
                return Ok(saved);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (!AuthHelper.IsHasAccess(User, id)) 
                return Forbid();

            var user = await _userService.GetById(id);

            if (user == null) 
                return NotFound();

            await _userService.Remove(user);

            return NoContent();
        }
    }
}
