using CE.DataAccess;
using CE.DataAccess.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CE.Service.Interfaces;
using CE.WebAPI.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(bool? fullInfo)
        {
            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool) fullInfo)
                {
                    return await _userService.GetAll(
                        null, null, null, 
                        u => u.Cars, 
                        u => u.Settings);
                }
                return await _userService.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<User>> GetUser(Guid id, bool? fullInfo)
        {
            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool)fullInfo)
                {
                    return await _userService.GetOne(
                        User, id,
                        u => u.Cars,
                        u => u.Settings);
                }
                return await _userService.GetOne(User, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(RegisterRequest request)
        {
            try
            {
                return await _userService.Create(User, request.GetUser());
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[Authorize(Roles = RolesConstants.Admin)]
        //[HttpPut("{id:Guid}")]
        //public async Task<IActionResult> EditUser(Guid id, PutUser request)
        //{
        //    var user = await _userService.FirstOrDefault(u => u.Id == id);

        //    if (user == null)
        //        return NotFound();

        //    request.UpdateUser(user);
        //    user.Password = _userService.GeneratePasswordHash(request.Password);

        //    try
        //    {
        //        await _userService.Update(user);
        //        return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPatch("{id:Guid}")]
        //public async Task<IActionResult> UpdateUser(Guid id, PatchUser user)
        //{
        //    if (!AuthHelper.IsHasAccess(User, id))
        //        return Forbid();

        //    var saved = await _userService.GetById(id);

        //    if (saved == null)
        //        return NotFound();

        //    try
        //    {
        //        await _userService.UpdatePartial(saved, user.GetUser());
        //        return Ok(saved);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                return await _userService.Delete(User, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
