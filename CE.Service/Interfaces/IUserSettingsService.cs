using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess.Dtos;
using CE.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Interfaces
{
    public interface IUserSettingsService
    {
        Task<GetUserSettingsDto> Create(Guid userId);

        Task<ActionResult<GetUserSettingsDto>> Create(ClaimsPrincipal claims, CreateUserSettingsDto dto);

        Task<ActionResult<GetUserSettingsDto>> GetOne(ClaimsPrincipal claims);

        Task<ActionResult<GetUserSettingsDto>> GetOne(ClaimsPrincipal claims, Guid id);

        Task<ActionResult<IEnumerable<GetUserSettingsDto>>> GetAll(ClaimsPrincipal claims,
            Expression<Func<UserSettings, bool>> filter = null,
            Func<IQueryable<UserSettings>, IOrderedQueryable<UserSettings>> orderBy = null,
            params Expression<Func<UserSettings, object>>[] includeProperties);

        Task<ActionResult> Update(ClaimsPrincipal claims, UpdateUserSettingsDto dto);

        Task<ActionResult> Delete(ClaimsPrincipal claims, Guid id);
    }
}
