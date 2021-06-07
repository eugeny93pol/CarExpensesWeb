using CE.DataAccess;
using CE.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CE.DataAccess.DTO;
using CE.Service.Interfaces;


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

        [HttpGet]
        public async Task<ActionResult<UserSettings>> GetUserSettings()
        {
            var userId = AuthHelper.GetUserId(User);

            return await _userSettingsService.FirstOrDefault(s => s.UserId == userId);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUserSettings([FromBody] UserSettingsDTO settings)
        {
            var userId = AuthHelper.GetUserId(User);
            var userSettings = await _userSettingsService.FirstOrDefault(s => s.UserId == userId);

            if (userSettings == null)
                return NotFound();

            try
            {
                await _userSettingsService.UpdatePartial(userSettings, settings);
                return Ok(userSettings);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
