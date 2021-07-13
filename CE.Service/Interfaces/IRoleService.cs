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
    public interface IRoleService
    {
        Task<ActionResult<GetRoleDto>> Create(ClaimsPrincipal claims, CreateRoleDto dto);

        Task<ActionResult<GetRoleDto>> GetOne(ClaimsPrincipal claims, Guid id,
            params Expression<Func<Role, object>>[] includeProperties);

        Task<ActionResult<IEnumerable<GetRoleDto>>> GetAll(
            ClaimsPrincipal claims = null,
            Expression<Func<Role, bool>> filter = null,
            Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = null,
            params Expression<Func<Role, object>>[] includeProperties);

        Task<ActionResult> Update(ClaimsPrincipal claims, UpdateRoleDto dto);

        Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id);
    }
}
