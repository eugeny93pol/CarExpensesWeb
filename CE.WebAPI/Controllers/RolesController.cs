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
    [Authorize(Roles = RolesConstants.Admin)]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<UsersController> _logger;

        public RolesController(IRoleService roleService, ILogger<UsersController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles(bool? fullInfo)
        {
            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool)fullInfo)
                {
                    return await _roleService.GetAll(User, null, null, r => r.Users);
                }
                return await _roleService.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Role>> GetRole(Guid id, bool? fullInfo)
        {
            try
            {
                fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
                if ((bool)fullInfo)
                {
                    return await _roleService.GetOne(User, id, r => r.Users);
                }
                return await _roleService.GetOne(User, id);
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
        public async Task<ActionResult<Role>> CreateRole([FromBody] Role role)
        {
            try
            {
                return await _roleService.Create(User, role);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region PUT
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Role>> EditRole(Guid id, Role role)
        {
            if (id != role.Id)
                return BadRequest("The route parameter 'id' does not match the 'id' parameter from body.");
            try
            {
                return await _roleService.Update(User, role);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion PUT

        #region DELETE
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await _roleService.Delete(User, id);
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
