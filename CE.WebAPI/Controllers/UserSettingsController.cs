using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CE.DataAccess.Dtos;
using CE.DataAccess.Models;
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
            return await _userSettingsService.GetUserSettings(User);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserSettings>> GetUserSettings(Guid id)
        {
            return await _userSettingsService.GetUserSettings(User, id);
        }

        [HttpPost]
        public async Task<ActionResult<UserSettings>> CreateUserSettings(UserSettingsDto settingsDto)
        {
            return await _userSettingsService.CreateUserSettings(User, settingsDto.AsDbModel());
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserSettings>> UpdateUserSettings(Guid id, UserSettingsDto settingsDto)
        {
            if (id != settingsDto.Id)
                return new BadRequestObjectResult(
                    "The route parameter 'id' does not match the 'id' parameter from body.");

            return await _userSettingsService.Update(User, settingsDto.AsDbModel());
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteUserSettings(Guid id)
        {
            return await _userSettingsService.Delete(User, id);
        }
    }
}
