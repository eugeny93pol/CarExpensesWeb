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
        private readonly IActionTypeService _actionTypeService;
        private readonly ILogger<UsersController> _logger;

        public ActionTypesController(IActionTypeService actionTypeService, ILogger<UsersController> logger)
        {
            _actionTypeService = actionTypeService;
            _logger = logger;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionType>>> GetTypes(bool? fullInfo)
        {
            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool)fullInfo)
                {
                    return await _actionTypeService.GetAll(User, null, null, a => a.Actions);
                }
                return await _actionTypeService.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ActionType>> GetType(Guid id, bool? fullInfo)
        {

            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool)fullInfo)
                {
                    return await _actionTypeService.GetOne(User, id, a => a.Actions);
                }
                return await _actionTypeService.GetOne(User, id);
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
        public async Task<ActionResult<ActionType>> CreateType([FromBody] ActionType action)
        {
            try
            {
                return await _actionTypeService.Create(User, action);
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
        public async Task<ActionResult<ActionType>> EditType(Guid id, ActionType action)
        {
            if (id != action.Id)
                return BadRequest("The route parameter 'id' does not match the 'id' parameter from body.");
            try
            {
                return await _actionTypeService.Update(User, action);
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
                return await _actionTypeService.Delete(User, id);
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
