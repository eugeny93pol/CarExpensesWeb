using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.DataAccess.Models;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionsController : ControllerBase
    {
        private readonly ICarActionService _carActionService;
        private readonly ILogger<UsersController> _logger;

        public ActionsController(ICarActionService carActionService, ILogger<UsersController> logger)
        {
            _carActionService = carActionService;
            _logger = logger;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAction>>> GetActions(Guid? carId, DateTime? from, DateTime? to)
        {
            try
            {
                @from ??= new DateTime();
                @to ??= DateTime.UtcNow;

                if (from > to)
                    return BadRequest("The date 'from' is greater than the date 'to'.");

                if (carId != null)
                {
                    return await _carActionService.GetActionsByCarId(
                        User, (Guid) carId,
                        a => a.Date >= from && a.Date <= to,
                        q => q.OrderByDescending(a => a.Date));
                }
                return await _carActionService.GetAll(
                    User,
                    a => a.Date >= from && a.Date <= to,
                    q => q.OrderByDescending(a => a.Date));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CarAction>> GetAction(Guid id)
        {
            try
            {
                return await _carActionService.GetOne(User, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion GET

        #region POST
        [HttpPost("repair")]
        public async Task<ActionResult<CarActionRepair>> CreateAction([FromBody] CarActionRepair action)
        {
            try
            {
                return await _carActionService.Create(User, action);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion POST
/*
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<CarAction>> EditAction(Guid id, CarAction action)
        {
            var saved = await _carActionService.FirstOrDefault(a => a.Id == id);

            if (saved == null)
                return NotFound();

            if (!await _carActionTypeService.IsActionTypeExist(action.Type))
                return BadRequest($"Type \"{action.Type}\" does not exist.");

            if (id != action.Id) 
                return BadRequest();

            var userId = AuthHelper.GetUserId(User);
            if (!(await _carService.IsUserHasAccessToCar(userId, saved.CarId) && 
                await _carService.IsUserHasAccessToCar(userId, action.CarId)))
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
            if (!await _carService.IsUserHasAccessToCar(userId, action.CarId))
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
            if (!await _carService.IsUserHasAccessToCar(userId, saved.CarId))
                return Forbid();

            if (action.Type != null && !await _carActionTypeService.IsActionTypeExist(action.Type))
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
        }*/
    }
}
