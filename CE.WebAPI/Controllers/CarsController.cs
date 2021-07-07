using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CE.DataAccess.Constants;
using CE.DataAccess.Models;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILogger<UsersController> _logger;

        public CarsController(ICarService carService, ILogger<UsersController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars(bool? fullInfo)
        {
            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool) fullInfo)
                {
                    return await _carService.GetCarsOfCurrentUser(
                        User, null, 
                        c => c.Settings, c => c.Actions);
                }
                return await _carService.GetCarsOfCurrentUser(User);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars(bool? fullInfo)
        {
            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool)fullInfo)
                {
                    return await _carService.GetAll(
                        User, null, null,
                        c => c.Settings, c => c.Actions);
                }
                return await _carService.GetAll(User);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Car>> GetCar(Guid id, bool? fullInfo)
        {
            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool)fullInfo)
                {
                    return await _carService.GetOne(
                        User, id,
                        c => c.Settings, c => c.Actions);
                }
                return await _carService.GetOne(User, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion GET

        #region POST
        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar(Car car)
        {
            try
            {
                return await _carService.Create(User, car);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion POST

        #region PUT
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Car>> UpdateCar(Guid id, Car car)
        {
            if (id != car.Id)
                return BadRequest("The route parameter 'id' does not match the 'id' parameter from body.");
            try
            {
                return await _carService.Update(User, car);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion PUT

        #region PATCH
        [HttpPatch("{id:Guid}")]
        public async Task<ActionResult<Car>> UpdatePartialCar(Guid id, Car car)
        {
            if (car.Id != Guid.Empty && id != car.Id)
                return BadRequest("The route parameter 'id' does not match the 'id' parameter from body.");
            try
            {
                car.Id = id;
                return await _carService.UpdatePartial(User, car);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion PATCH

        #region DELETE
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            try
            {
                return await _carService.Delete(User, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion DELETE
    }
}
