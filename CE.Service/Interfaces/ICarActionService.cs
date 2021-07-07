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
    public interface ICarActionService
    {
        Task<ActionResult<T>> Create<T>(ClaimsPrincipal claims, T item) 
            where T : CarAction;
        
        Task<ActionResult<T>> GetOne<T>(
            ClaimsPrincipal claims, 
            Guid id,
            params Expression<Func<T, object>>[] includeProperties) 
            where T : CarAction;

        Task<ActionResult<IEnumerable<T>>> GetAll<T>(
            ClaimsPrincipal claims = null,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties) 
            where T : CarAction;

        Task<ActionResult<IEnumerable<T>>> GetActionsByCarId<T>(
            ClaimsPrincipal claims, 
            Guid carId,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties) 
            where T : CarAction;

        Task<ActionResult<T>> Update<T>(ClaimsPrincipal claims, T item) 
            where T : CarAction;

        Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id);
    }
}
