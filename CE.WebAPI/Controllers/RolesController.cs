using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CE.DataAccess.Constants;
using CE.DataAccess.Models;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RolesConstants.Admin)]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles(bool? fullInfo)
        {
            fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
            if ((bool)fullInfo)
            {
                return await _roleService.GetAll(User, null, null, r => r.Users);
            }
            return await _roleService.GetAll();
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Role>> GetRole(Guid id, bool? fullInfo)
        {
            fullInfo ??= Request.Query.Keys.Contains(nameof(fullInfo));
            if ((bool)fullInfo)
            {
                return await _roleService.GetOne(User, id, r => r.Users);
            }
            return await _roleService.GetOne(User, id);
        }
        #endregion GET

        #region POST
        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole([FromBody] Role role)
        {
            return await _roleService.Create(User, role);
        }
        #endregion

        #region PUT
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Role>> EditRole(Guid id, Role role)
        {
            if (id != role.Id)
                return BadRequest("The route parameter 'id' does not match the 'id' parameter from body.");

            return await _roleService.Update(User, role);
        }
        #endregion PUT

        #region DELETE
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _roleService.Delete(User, id);
        }
        #endregion DELETE

    }
}
