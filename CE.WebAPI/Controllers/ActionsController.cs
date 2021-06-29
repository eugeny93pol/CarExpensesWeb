using CE.DataAccess;
using CE.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.Service.Interfaces;
using CE.WebAPI.RequestModels;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionsController : ControllerBase
    {
        private readonly ICarActionService _carActionService;
        private readonly ICarService _carService;
        private readonly IActionTypeService _actionTypeService;

        public ActionsController(
            ICarActionService carActionService, 
            ICarService carService, 
            IActionTypeService actionTypeService)
        {
            _carActionService = carActionService;
            _carService = carService;
            _actionTypeService = actionTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAction>>> FilterByDate(Guid? carId, DateTime? from, DateTime? to)
        {
            @from ??= new DateTime();
            @to ??= DateTime.UtcNow;

            if (from > to)
                return BadRequest("The date 'from' is greater than the date 'to'.");

            var userId = AuthHelper.GetUserId(User);
            var userCarsIds = await _carService.GetCarsIdsByUserId(userId);

            if (carId != null && !userCarsIds.Contains((Guid)carId))
                return Forbid();

            var actions = await _carActionService.GetAll(
                a => (carId != null ? a.CarId == carId : userCarsIds.Contains(a.CarId)) 
                     && a.Date >= from && a.Date <= to,
                q => q.OrderByDescending(a => a.Date));

            return ActionsWithSummary(actions);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CarAction>> GetAction(Guid id)
        {
            var userId = AuthHelper.GetUserId(User);
            var action = await _carActionService.GetById(id);

            if (action == null) 
                return NotFound();

            if (!await _carService.IsUserOwnerCar(userId, action.CarId))
                return Forbid();

            return action;
        }

        [HttpPost]
        public async Task<ActionResult<CarAction>> CreateAction([FromBody] CarAction action)
        {
            if (!await _actionTypeService.IsActionTypeExist(action.Type))
                return BadRequest($"Type \"{action.Type}\" does not exist.");

            var userId = AuthHelper.GetUserId(User);

            if (!await _carService.IsUserOwnerCar(userId, action.CarId))
                return Forbid();

            try
            {
                await _carActionService.Create(action);
                return CreatedAtAction(nameof(GetAction), new {id = action.Id}, action);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<CarAction>> EditAction(Guid id, CarAction action)
        {
            var saved = await _carActionService.FirstOrDefault(a => a.Id == id);

            if (saved == null)
                return NotFound();

            if (!await _actionTypeService.IsActionTypeExist(action.Type))
                return BadRequest($"Type \"{action.Type}\" does not exist.");

            if (id != action.Id) 
                return BadRequest();

            var userId = AuthHelper.GetUserId(User);
            if (!(await _carService.IsUserOwnerCar(userId, saved.CarId) && 
                await _carService.IsUserOwnerCar(userId, action.CarId)))
                return Forbid();

            try
            {
                await _carActionService.Update(action);
                return Ok(action);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAction(Guid id)
        {
            var action = await _carActionService.GetById(id);
            if (action == null)
                return NotFound();
            
            var userId = AuthHelper.GetUserId(User);
            if (!await _carService.IsUserOwnerCar(userId, action.CarId))
                return Forbid();

            await _carActionService.Remove(action);

            return NoContent();
        }

        [HttpPatch("{id:Guid}")]
        public async Task<ActionResult<CarAction>> UpdateAction(Guid id, PatchCarAction action)
        {
            var saved = await _carActionService.GetById(id);
            if (saved == null)
                return NotFound();

            var userId = AuthHelper.GetUserId(User);
            if (!await _carService.IsUserOwnerCar(userId, saved.CarId))
                return Forbid();

            if (action.Type != null && !await _actionTypeService.IsActionTypeExist(action.Type))
                return BadRequest($"Type \"{action.Type}\" does not exist.");

            try
            {
                await _carActionService.UpdatePartial(saved, action.GetAction());
                return Ok(saved);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private OkObjectResult ActionsWithSummary(IEnumerable<CarAction> actions)
        {
            var actionsList = actions.ToList();

            var firstDate = actionsList.Min(a => a.Date);
            var lastDate = actionsList.Max(a => a.Date);
            var totalAmount = actionsList.Sum(a => a.Amount);
            var averageAmount = actionsList.Average(a => a.Amount);

            var summary = new
            {
                firstDate,
                lastDate,
                totalAmount,
                averageAmount,
                actionsList.Count
            };

            return Ok(new { actionsList, summary });
        }
    }
}
