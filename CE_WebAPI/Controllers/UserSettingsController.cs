using CE.DataAccess;
using CE.Service;
using CE.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserSettingsController : ControllerBase
    {

        private readonly IUserSettingsService _userSettingsService;

        public UserSettingsController(IUserSettingsService userSettingsService)
        {
            _userSettingsService = userSettingsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserSettings>> GetUserSettings(long id)
        {
            if (!AuthHelper.IsHasAccess(User, id)) { return Forbid(); }

            var userSettings = await _userSettingsService.FirstOrDefault(s => s.UserId == id);

            return userSettings == null ? NotFound() : userSettings;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartial(long id, [FromBody] UserSettingsDTO settings)
        {
            if (!AuthHelper.IsHasAccess(User, id)) { return Forbid(); }

            var userSettings = await _userSettingsService.GetAsNoTracking(s => s.UserId == id);

            if (userSettings == null) { return NotFound(); }

            try
            {
                await _userSettingsService.UpdatePartial(userSettings, settings);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
