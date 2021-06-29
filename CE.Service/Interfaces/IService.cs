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
    public interface IService<T> where T : BaseEntity
    {
        Task<ActionResult<T>> Create(ClaimsPrincipal claims, T item);

        Task<ActionResult<T>> GetOne(
            ClaimsPrincipal claims, 
            Guid id, 
            params Expression<Func<T, object>>[] includeProperties);

        //Task<ActionResult<T>> GetOneWhere(Expression<Func<T, bool>> filter);

        Task<ActionResult<IEnumerable<T>>> GetAll(
            ClaimsPrincipal claims = null,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties);

        Task<ActionResult<T>> Update(ClaimsPrincipal claims, T item);

        Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id);
    }
}
