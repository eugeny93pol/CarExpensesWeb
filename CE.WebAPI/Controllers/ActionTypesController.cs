using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CE.DataAccess;
using CE.DataAccess.Constants;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionTypesController : ControllerBase
    {
        private readonly ICarActionTypeService _carActionTypeService;
        private readonly ILogger<UsersController> _logger;

        public ActionTypesController(ICarActionTypeService carActionTypeService, ILogger<UsersController> logger)
        {
            _carActionTypeService = carActionTypeService;
            _logger = logger;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarActionType>>> GetTypes(bool? fullInfo)
        {
            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool)fullInfo)
                {
                    return await _carActionTypeService.GetAll(User, null, null, a => a.Actions);
                }
                return await _carActionTypeService.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CarActionType>> GetType(Guid id, bool? fullInfo)
        {

            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool)fullInfo)
                {
                    return await _carActionTypeService.GetOne(User, id, a => a.Actions);
                }
                return await _carActionTypeService.GetOne(User, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion GET

        #region POST
        [Authorize(Roles = RolesConstants.Admin)]
        [HttpPost]
        public async Task<ActionResult<CarActionType>> CreateType([FromBody] CarActionType carActionType)
        {
            try
            {
                return await _carActionTypeService.Create(User, carActionType);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion POST

        #region PUT
        [Authorize(Roles = RolesConstants.Admin)]
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<CarActionType>> EditType(Guid id, CarActionType carActionType)
        {
            if (id != carActionType.Id)
                return BadRequest("The route parameter 'id' does not match the 'id' parameter from body.");
            try
            {
                return await _carActionTypeService.Update(User, carActionType);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion PUT

        #region DELETE
        [Authorize(Roles = RolesConstants.Admin)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await _carActionTypeService.Delete(User, id);
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
