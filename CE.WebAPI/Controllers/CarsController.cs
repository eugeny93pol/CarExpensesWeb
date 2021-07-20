using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CE.DataAccess.Constants;
using CE.DataAccess.Dtos;
using CE.Service.Interfaces;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars(bool? fullInfo)
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

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetAllCars(bool? fullInfo)
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

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CarDto>> GetCar(Guid id, bool? fullInfo)
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
        #endregion GET

        #region POST
        [HttpPost]
        public async Task<ActionResult<CarDto>> CreateCar(CreateCarDto car)
        {
            return await _carService.Create(User, car);
        }
        #endregion POST

        #region PUT
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateCar(Guid id, UpdateCarDto car)
        {
            if (id != car.Id)
                return BadRequest("The route parameter 'id' does not match the 'id' parameter from body.");

            return await _carService.Update(User, car);
        }
        #endregion PUT

        #region DELETE
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            return await _carService.Delete(User, id);
        }
        #endregion DELETE
    }
}
