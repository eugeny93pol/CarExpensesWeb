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
    public interface ICarActionService : IService<CarAction>
    {
        Task<ActionResult<IEnumerable<CarAction>>> GetActionsByCarId(
            ClaimsPrincipal claims,
            Guid carId,
            Expression<Func<CarAction, bool>> filter = null,
            Func<IQueryable<CarAction>, IOrderedQueryable<CarAction>> orderBy = null,
            params Expression<Func<CarAction, object>>[] includeProperties);

        Task UpdatePartial(CarAction savedAction, CarAction action);
    }
}
