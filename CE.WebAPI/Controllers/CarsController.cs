using CE.DataAccess;
using CE.DataAccess.Constants;
using CE.Service;
using CE.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
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
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ICarSettingsService _carSettingsService;

        public CarsController(ICarService carService, ICarSettingsService carSettingsService)
        {
            _carService = carService;
            _carSettingsService = carSettingsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars([FromQuery] string[] include)
        {
            IEnumerable<Car> cars;
            if (User.IsInRole(RolesConstants.Admin))
            {
                cars = await _carService.GetAll(includeProperties: include);
            } else
            {
                var userId = AuthHelper.GetUserID(User);
                cars = await _carService.GetAll(filter: c => c.UserId == userId, includeProperties: include);
            }

            return cars.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(long id, [FromQuery] string[] include)
        {
            var car = include.Length != 0 ?
                await _carService.GetById(id, include) :
                await _carService.GetById(id);

            if (car != null && !AuthHelper.IsHasAccess(User, car.UserId)) { return Forbid(); }

            return car == null ? NotFound() : car;
        }

        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar(Car car)
        {
            if (car.UserId == 0) return BadRequest();

            car.UserId = AuthHelper.GetUserID(User);
            
            await _carService.Create(car);
            await _carSettingsService.Create(new CarSettings(car.Id));

            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCar(long id, Car car)
        {

            if (id != car.Id) { return BadRequest(); }

            var saved = await _carService.GetAsNoTracking(c => c.Id == id);

            if (saved == null) { return NotFound(); }

            if (!(AuthHelper.IsHasAccess(User, car.UserId)
                && AuthHelper.IsHasAccess(User, saved.UserId))) 
            { 
                return Forbid(); 
            }

            try
            {
                await _carService.Update(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(long id)
        {
            var car = await _carService.GetById(id);

            if (car == null) { return NotFound(); }

            if (!AuthHelper.IsHasAccess(User, car.UserId)) { return Forbid(); }

            await _carService.Remove(car);

            return NoContent();
        }
    }
}
