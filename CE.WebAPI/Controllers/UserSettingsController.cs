using CE.DataAccess;
using CE.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CE.Service.Interfaces;
using CE.WebAPI.RequestModels;


namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserSettingsController : ControllerBase
    {

        private readonly IUserSettingsService _userSettingsService;
        private readonly ICarService _carService;

        public UserSettingsController(IUserSettingsService userSettingsService,
            ICarService carService)
        {
            _userSettingsService = userSettingsService;
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<UserSettings>> GetUserSettings()
        {
            var userId = AuthHelper.GetUserId(User);

            return await _userSettingsService.FirstOrDefault(s => s.UserId == userId);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUserSettings([FromBody] PatchUserSettings settings)
        {
            var userId = AuthHelper.GetUserId(User);

            if (settings.DefaultCarId != null &&
                !await _carService.IsUserOwnerCar(userId, (Guid) settings.DefaultCarId))
                return Forbid();

            var userSettings = await _userSettingsService.FirstOrDefault(s => s.UserId == userId);

            if (userSettings == null)
                return NotFound();

            try
            {
                await _userSettingsService.UpdatePartial(userSettings, settings.GetUserSettings());
                return Ok(userSettings);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
