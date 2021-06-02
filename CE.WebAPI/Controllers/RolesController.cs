using CE.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.Service.Interfaces;


namespace CE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles([FromQuery] string[] include)
        {
            var roles = await _roleService.GetAll(includeProperties: include);
            return roles.ToList();
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Role>> GetRole(long id, [FromQuery] string[] include)
        {
            var role = include.Length != 0 ?
                await _roleService.GetById(id, include) :
                await _roleService.GetById(id);
            return role == null ? NotFound() : role;
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
            {
                return BadRequest();
            }

            try
            {
                await _roleService.Update(role);
            }
            catch
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var role = await _roleService.GetById(id);
            if (role == null)
            {
                return NotFound();
            }

            await _roleService.Remove(role);

            return NoContent();
        }
    
    }
}
