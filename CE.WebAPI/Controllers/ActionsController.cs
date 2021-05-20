using CE.DataAccess;
using CE.DataAccess.Constants;
using CE.Service;
using CE.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionsController : ControllerBase
    {
        private readonly ICarActionService _carActionService;
        private readonly ICarService _carService;

        public ActionsController(ICarActionService carActionService, ICarService carService)
        {
            _carActionService = carActionService;
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAction>>> GetActions()
        {
            long userId = AuthHelper.GetUserID(User);
            long[] userCarsIds = await _carService.GetCarsIdsByUserId(userId);

            var actions = await _carActionService.GetAll(a => userCarsIds.Contains(a.CarId));

            return actions.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarAction>> GetAction(long id)
        {
            var userId = AuthHelper.GetUserID(User);
            var action = await _carActionService.GetById(id);

            if (action == null) { return NotFound(); }

            if (!(await _carService.IsUserOwnerCar(userId, action.CarId)))
            {
                return Forbid();
            }

            return action;
        }

        [HttpPost]
        public async Task<ActionResult<CarAction>> CreateAction([FromBody] CarAction action)
        {
            var userId = AuthHelper.GetUserID(User);

            if (!(await _carService.IsUserOwnerCar(userId, action.CarId)))
            {
                return BadRequest();
            }

            await _carActionService.Create(action);

            return CreatedAtAction(nameof(GetAction), new { id = action.Id }, action);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAction(long id, CarAction action)
        {
            if (id != action.Id) { return BadRequest(); }

            var saved = await _carActionService.GetAsNoTracking(a => a.Id == id);

            if (saved == null) { return NotFound(); }

            var userId = AuthHelper.GetUserID(User);
            if (!(await _carService.IsUserOwnerCar(userId, saved.CarId) && 
                await _carService.IsUserOwnerCar(userId, action.CarId)))
            {
                return Forbid();
            }

            try
            {
                await _carActionService.Update(action);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAction(long id)
        {
            var action = await _carActionService.GetById(id);
            if (action == null)
            {
                return NotFound();
            }

            var userId = AuthHelper.GetUserID(User);
            if (!(await _carService.IsUserOwnerCar(userId, action.CarId)))
            {
                return Forbid();
            }

            await _carActionService.Remove(action);

            return NoContent();
        }
    }
}
