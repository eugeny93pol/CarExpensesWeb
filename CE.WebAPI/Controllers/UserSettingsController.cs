using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CE.DataAccess.Constants;
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

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserSettingsDto>>> GetAllSettings()
        {
            return await _userSettingsService.GetAll(User);
        }

        [Authorize(Roles = RolesConstants.User)]
        [HttpGet]
        public async Task<ActionResult<GetUserSettingsDto>> GetUserSettings()
        {
            return await _userSettingsService.GetOne(User);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetUserSettingsDto>> GetUserSettings(Guid id)
        {
            return await _userSettingsService.GetOne(User, id);
        }

        [HttpPost]
        public async Task<ActionResult<GetUserSettingsDto>> CreateUserSettings(CreateUserSettingsDto settingsDto)
        {
            return await _userSettingsService.Create(User, settingsDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserSettings>> UpdateUserSettings(Guid id, UpdateUserSettingsDto settingsDto)
        {
            if (id != settingsDto.Id)
                return new BadRequestObjectResult(
                    "The route parameter 'id' does not match the 'id' parameter from body.");

            return await _userSettingsService.Update(User, settingsDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteUserSettings(Guid id)
        {
            return await _userSettingsService.Delete(User, id);
        }
    }
}
