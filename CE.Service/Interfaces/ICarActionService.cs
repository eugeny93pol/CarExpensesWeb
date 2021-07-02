﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess.Models;
using CE.Service.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Interfaces
{
    public interface ICarActionService
    {
        IService<CarActionRepair> Repair { get; }

        Task<ActionResult<T>> Create<T>(ClaimsPrincipal claims, T item) where T : CarAction;
        Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id);



        Task<ActionResult<IEnumerable<CarAction>>> GetActionsByCarId(
            ClaimsPrincipal claims,
            Guid carId,
            Expression<Func<CarAction, bool>> filter = null,
            Func<IQueryable<CarAction>, IOrderedQueryable<CarAction>> orderBy = null,
            params Expression<Func<CarAction, object>>[] includeProperties);

        Task UpdatePartial(CarAction savedAction, CarAction action);
    }
}
