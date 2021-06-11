using CE.DataAccess;
using CE.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.Service.Interfaces;


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
        public async Task<IEnumerable<Car>> GetCars()
        {
            var userId = AuthHelper.GetUserId(User);
            return await _carService.GetCarsByUserId(userId);
        }

        [HttpGet("info")]
        public async Task<IEnumerable<Car>> GetCarsFullInfo()
        {
            var userId = AuthHelper.GetUserId(User);
            return await _carService
                .GetAll(c => c.UserId == userId, null, c => c.Settings, c => c.Actions);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            var car = await _carService.GetById(id);

            if (!AuthHelper.IsHasAccess(User, car?.UserId))
                return Forbid();
            
            return car != null ? Ok(car) : NotFound();
        }

        [HttpGet("{id:Guid}/info")]
        public async Task<ActionResult<Car>> GetCarFullInfo(Guid id)
        {
            var car = await _carService.GetById(id, c => c.Settings, c => c.Actions);

            if (!AuthHelper.IsHasAccess(User, car?.UserId))
                return Forbid();

            return car != null ? Ok(car) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar(Car car)
        {
            car.UserId = AuthHelper.GetUserId(User);
            
            await _carService.Create(car);
            await _carSettingsService.Create(new CarSettings(car.Id));

            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> EditCar(Guid id, Car car)
        {

            if (id != car.Id) 
                return BadRequest();

            var saved = await _carService.FirstOrDefault(c => c.Id == id);

            if (!(AuthHelper.IsHasAccess(User, car.UserId) && 
                  AuthHelper.IsHasAccess(User, saved?.UserId))) 
                return Forbid();

            if (saved == null) 
                return NotFound();

            try
            {
                await _carService.Update(car);
                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            var car = await _carService.GetById(id);

            if (!AuthHelper.IsHasAccess(User, car?.UserId)) 
                return Forbid();

            if (car == null) 
                return NotFound();

            await _carService.Remove(car);

            return NoContent();
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> UpdateCar(Guid id, Car car)
        {
            var saved = await _carService.FirstOrDefault(c => c.Id == id);

            if (!(AuthHelper.IsHasAccess(User, car.UserId) && 
                  AuthHelper.IsHasAccess(User, saved?.UserId)))
                return Forbid();

            if (saved == null) 
                return NotFound();
            
            try
            {
                await _carService.UpdatePartial(saved, car);
                return Ok(saved);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
