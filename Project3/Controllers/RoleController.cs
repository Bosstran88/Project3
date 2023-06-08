using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        IRoleService roleService;
        public RoleController(IRoleService _roleService) 
        {
            roleService = _roleService;
        }

        [HttpPost("createOrUpdate") , Authorize]
        public async Task<IActionResult> createOrUpdate([FromBody] AddRoleReq addRole)
        {
            return Ok(roleService.createOrUpdate(addRole));
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> getById(long Id)
        {
            return Ok(roleService.getOne(Id));
        }

        [HttpDelete("deleteById/{id}") , Authorize]
        public async Task<IActionResult> deleteById(long id)
        {
            return Ok(roleService.deleteById(id));
        }

        [HttpPost("search")]
        public async Task<IActionResult> search([FromBody] RoleReq filter)
        {
            return Ok(roleService.getPagin(filter));
        }
    }
}
