using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Interfaces
{
    public interface IUserSettingsService
    {
        Task<UserSettings> CreateUserSettings(Guid userId);

        Task<ActionResult<UserSettings>> CreateUserSettings(ClaimsPrincipal claims, UserSettings item);

        Task<ActionResult<UserSettings>> GetUserSettings(ClaimsPrincipal claims);

        Task<ActionResult<UserSettings>> GetUserSettings(ClaimsPrincipal claims, Guid id);

        Task<ActionResult<UserSettings>> Update(ClaimsPrincipal claims, UserSettings item);

        Task<ActionResult> Delete(ClaimsPrincipal claims, Guid id);
    }
}
