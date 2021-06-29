using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Interfaces
{
    public interface ICarService : IService<Car>
    {
        Task<ActionResult<IEnumerable<Car>>> GetCarsOfCurrentUser(
            ClaimsPrincipal claims,
            Func<IQueryable<Car>, IOrderedQueryable<Car>> orderBy = null,
            params Expression<Func<Car, object>>[] includeProperties);

        Task<ActionResult<Car>> UpdatePartial(ClaimsPrincipal claims, Car item);

        Task<Guid[]> GetCarsIdsByUserId(Guid id);

        Task<bool> IsUserOwnerCar(Guid userId, Guid carId);
    }
}
