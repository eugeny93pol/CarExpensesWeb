using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.DataAccess;
using CE.DataAccess.Constants;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionTypesController : ControllerBase
    {
        private readonly IActionTypeService _actionTypeService;

        public ActionTypesController(IActionTypeService actionTypeService)
        {
            _actionTypeService = actionTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionType>>> GetTypes()
        {
            var actions = await _actionTypeService.GetAll();
            return actions.ToList();
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ActionType>> GetType(Guid id)
        {
            var action = await _actionTypeService.GetById(id);
            return action != null ? Ok(action) : NotFound();
        }

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpPost]
        public async Task<ActionResult<ActionType>> CreateType([FromBody] ActionType action)
        {
            await _actionTypeService.Create(action);
            return CreatedAtAction(nameof(GetType), new { id = action.Id }, action);
        }

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> EditType(Guid id, ActionType action)
        {
            if (id != action.Id)
                return BadRequest();

            try
            {
                await _actionTypeService.Update(action);
                return Ok(action);
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var action = await _actionTypeService.GetById(id);
            if (action == null)
                return NotFound();

            await _actionTypeService.Remove(action);

            return NoContent();
        }
    }
}
