using CE.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.DataAccess.Constants;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;


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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _roleService.GetAll();
            return roles.ToList();
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Role>> GetRole(long id)
        {
            var role = await _roleService.GetById(id);
            return role != null ? Ok(role) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole([FromBody] Role role)
        {
            await _roleService.Create(role);

            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> EditRole(long id, Role role)
        {
            if (id != role.Id)
                return BadRequest();

            try
            {
                await _roleService.Update(role);
                return Ok(role);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var role = await _roleService.GetById(id);
            if (role == null)
                return NotFound();

            await _roleService.Remove(role);

            return NoContent();
        }
    
    }
}
