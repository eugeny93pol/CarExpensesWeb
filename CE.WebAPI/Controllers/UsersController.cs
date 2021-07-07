using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CE.DataAccess.Constants;
using CE.DataAccess.Models;
using CE.Service.Interfaces;
using CE.WebAPI.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        #region GET
        [Authorize(Roles = RolesConstants.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(bool? fullInfo)
        {
            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool)fullInfo)
                {
                    return await _userService.GetAll(
                        null, null, null, 
                        u => u.Cars, u => u.Settings);
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
                        u => u.Cars, u => u.Settings);
                }
                return await _userService.GetOne(User, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion GET

        #region POST
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
        #endregion POST

        #region PUT
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<User>> UpdateUser(Guid id, PutUser request)
        {
            if (id != request.Id)
                return BadRequest("The route parameter 'id' does not match the 'id' parameter from body.");
            try
            {
                return await _userService.Update(User, request.ConvertToUser());
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion PUT

        #region PATH
        [HttpPatch("{id:Guid}")]
        public async Task<ActionResult<User>> UpdatePartialUser(Guid id, PatchUser patchUser)
        {
            if (id != patchUser.Id)
                return BadRequest("The route parameter 'id' does not match the 'id' parameter from body.");
            try
            {
                var user = patchUser.ConvertToUser();
                return await _userService.UpdatePartial(User, user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion PATH

        #region DELETE
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
        #endregion DELETE
    }
}
