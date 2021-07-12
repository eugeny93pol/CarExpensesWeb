using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CE.DataAccess.Constants;
using CE.DataAccess.Dtos;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #region GET
        [Authorize(Roles = RolesConstants.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(bool? fullInfo)
        {
            fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
            if ((bool)fullInfo)
            {
                return await _userService.GetAll(
                    null, null, null, 
                    u => u.Cars, u => u.Settings);
            }
            return await _userService.GetAll(User);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id, bool? fullInfo)
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
        #endregion GET

        #region POST
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto userDto)
        {
            return await _userService.Create(User, userDto);
        }
        #endregion POST

        #region PUT
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateUser(Guid id, UpdateUserDto userDto)
        {
            if (id != userDto.Id)
                return BadRequest("The route parameter 'id' does not match the 'id' parameter from body.");
           
            return await _userService.Update(User, userDto);
        }
        #endregion PUT

        #region DELETE
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            return await _userService.Delete(User, id);
        }
        #endregion DELETE
    }
}
