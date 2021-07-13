using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Interfaces
{
    public interface IRoleService
    {
        Task<ActionResult<Role>> Create(ClaimsPrincipal claims, Role item);

        Task<ActionResult<Role>> GetOne(ClaimsPrincipal claims, Guid id,
            params Expression<Func<Role, object>>[] includeProperties);

        Task<ActionResult<IEnumerable<Role>>> GetAll(
            ClaimsPrincipal claims = null,
            Expression<Func<Role, bool>> filter = null,
            Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = null,
            params Expression<Func<Role, object>>[] includeProperties);

        Task<ActionResult<Role>> Update(ClaimsPrincipal claims, Role item);

        Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id);
    }
}
