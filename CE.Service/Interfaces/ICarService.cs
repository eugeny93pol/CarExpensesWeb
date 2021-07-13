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
    public interface ICarService
    {
        Task<ActionResult<CarDto>> Create(ClaimsPrincipal claims, CreateCarDto dto);

        Task<ActionResult<CarDto>> GetOne(ClaimsPrincipal claims, Guid id,
            params Expression<Func<Car, object>>[] includeProperties);

        Task<ActionResult<IEnumerable<CarDto>>> GetAll(ClaimsPrincipal claims,
            Expression<Func<Car, bool>> filter = null,
            Func<IQueryable<Car>, IOrderedQueryable<Car>> orderBy = null,
            params Expression<Func<Car, object>>[] includeProperties);

        Task<ActionResult<IEnumerable<CarDto>>> GetCarsOfCurrentUser(
            ClaimsPrincipal claims,
            Func<IQueryable<Car>, IOrderedQueryable<Car>> orderBy = null,
            params Expression<Func<Car, object>>[] includeProperties);

        Task<ActionResult> Update(ClaimsPrincipal claims, UpdateCarDto dto);

        Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id);

        Task<Guid[]> GetCarsIdsOfCurrentUser(ClaimsPrincipal claims);

        Task<bool> IsUserHasAccessToCar(ClaimsPrincipal claims, Guid carId, Car car = null);
    }
}
