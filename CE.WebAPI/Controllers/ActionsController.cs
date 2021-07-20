using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CE.DataAccess.Constants;
using CE.DataAccess.Models;
using CE.Service.Interfaces;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionsController : ControllerBase
    {
        private readonly ICarActionService _carActionService;

        public ActionsController(ICarActionService carActionService)
        {
            _carActionService = carActionService;
        }

        #region GET
        #region GET_MANY
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAction>>> GetActions(Guid? carId, DateTime? from, DateTime? to)
        {
            return await GetAllActionsOfType<CarAction>(carId, from, to);
        }

        [HttpGet(CarActionTypesConstants.Mileage)]
        public async Task<ActionResult<IEnumerable<CarActionMileage>>> GetMileageActions(Guid? carId, DateTime? from, DateTime? to)
        {
            return await GetAllActionsOfType<CarActionMileage>(carId, from, to);
        }

        [HttpGet(CarActionTypesConstants.Refill)]
        public async Task<ActionResult<IEnumerable<CarActionRefill>>> GetRefillActions(Guid? carId, DateTime? from, DateTime? to)
        {
            return await GetAllActionsOfType<CarActionRefill>(carId, from, to);
        }

        [HttpGet(CarActionTypesConstants.Repair)]
        public async Task<ActionResult<IEnumerable<CarActionRepair>>> GetRepairActions(Guid? carId, DateTime? from, DateTime? to)
        {
            return await GetAllActionsOfType<CarActionRepair>(carId, from, to, r => r.SpareParts);
        }
        #endregion GET_MANY

        #region GET_ONE
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CarAction>> GetAction(Guid id)
        {
            return await GetActionOfType<CarAction>(id);
        }

        [HttpGet(CarActionTypesConstants.Mileage+"/{id:Guid}")]
        public async Task<ActionResult<CarActionMileage>> GetMileageAction(Guid id)
        {
            return await GetActionOfType<CarActionMileage>(id);
        }

        [HttpGet(CarActionTypesConstants.Refill+"/{id:Guid}")]
        public async Task<ActionResult<CarActionRefill>> GetRefillAction(Guid id)
        {
            return await GetActionOfType<CarActionRefill>(id);
        }

        [HttpGet(CarActionTypesConstants.Repair+"/{id:Guid}")]
        public async Task<ActionResult<CarActionRepair>> GetRepairAction(Guid id)
        {
            return await GetActionOfType<CarActionRepair>(id, r => r.SpareParts);
        }
        #endregion GET_ONE
        #endregion GET

        #region POST
        [HttpPost]
        public async Task<ActionResult<CarAction>> Create(CarAction action)
        {
            return await CreateActionOfType(action);
        }

        [HttpPost(CarActionTypesConstants.Mileage)]
        public async Task<ActionResult<CarActionMileage>> Create(CarActionMileage action)
        {
            return await CreateActionOfType(action);
        }

        [HttpPost(CarActionTypesConstants.Refill)]
        public async Task<ActionResult<CarActionRefill>> Create(CarActionRefill action)
        {
            return await CreateActionOfType(action);
        }

        [HttpPost(CarActionTypesConstants.Repair)]
        public async Task<ActionResult<CarActionRepair>> Create(CarActionRepair action)
        {
            return await CreateActionOfType(action);
        }
        #endregion POST

        #region PUT
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, CarAction action)
        {
            return await UpdateAction(id, action);
        }

        [HttpPut(CarActionTypesConstants.Mileage + "/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, CarActionMileage action)
        {
            return await UpdateAction(id, action);
        }

        [HttpPut(CarActionTypesConstants.Refill + "/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, CarActionRefill action)
        {
            return await UpdateAction(id, action);
        }

        [HttpPut(CarActionTypesConstants.Repair + "/{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, CarActionRepair action)
        {
            return await UpdateAction(id, action);
        }
        #endregion PUT

        #region DELETE
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAction(Guid id)
        {
            return await _carActionService.Delete(User, id);
        }
        #endregion DELETE


        #region GENERIC_FUNCTIUONS
        private async Task<ActionResult<T>> CreateActionOfType<T>(T action) where T : CarAction
        {
            return await _carActionService.Create(User, action);
        }

        private async Task<ActionResult<T>> GetActionOfType<T>(
            Guid id, params Expression<Func<T, object>>[] includeProperties) where T : CarAction
        {
            return await _carActionService.GetOne<T>(User, id, includeProperties);
        }

        private async Task<ActionResult<IEnumerable<T>>> GetAllActionsOfType<T>(
            Guid? carId, DateTime? from, DateTime? to,
            params Expression<Func<T, object>>[] includeProperties) where T : CarAction
        {
            @from ??= new DateTime();
            @to ??= DateTime.UtcNow;

            if (from > to)
                return BadRequest("The date 'from' is greater than the date 'to'.");

            if (carId != null)
            {
                return await _carActionService.GetActionsByCarId<T>(
                    User, (Guid)carId,
                    a => a.Date >= from && a.Date <= to,
                    q => q.OrderByDescending(a => a.Date),
                    includeProperties);
            }
            return await _carActionService.GetAll<T>(
                User,
                a => a.Date >= from && a.Date <= to,
                q => q.OrderByDescending(a => a.Date),
                includeProperties);
        }

        private async Task<IActionResult> UpdateAction<T>(Guid id, T action) where T : CarAction
        {
            if (id != action.Id)
                return BadRequest("The route parameter 'id' does not match the 'id' parameter from body.");

            return await _carActionService.Update(User, action);
        }
        #endregion GENERICS_FUNCTIONS
    }
}
