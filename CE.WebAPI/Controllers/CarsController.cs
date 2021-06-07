using CE.DataAccess;
using CE.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Car>> GetCarById(long id)
        {
            var car = await _carService.GetById(id);

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

            return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> EditCar(long id, Car car)
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

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteCar(long id)
        {
            var car = await _carService.GetById(id);

            if (!AuthHelper.IsHasAccess(User, car?.UserId)) 
                return Forbid();

            if (car == null) 
                return NotFound();

            await _carService.Remove(car);

            return NoContent();
        }

        [HttpPatch("{id:long}")]
        public async Task<IActionResult> UpdateCar(long id, Car car)
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
