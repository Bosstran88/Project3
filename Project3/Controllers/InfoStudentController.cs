using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Migrations;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoStudentController : ControllerBase
    {
        IInformationStudentService _infoService;
        public InfoStudentController(IInformationStudentService infoService)
        {
            _infoService = infoService;
        }

        [HttpPost("createOrUpdate")]
        public async Task<IActionResult> createOrUpdate([FromBody] AddInfomationStudentReq? menuReq)
        {
            return Ok(_infoService.createOrUpdate(menuReq));
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> getById(long Id)
        {
            return Ok(_infoService.getOne(Id));
        }

        [HttpDelete("deleteById/{menuId}")]
        public async Task<IActionResult> deleteById(long menuId)
        {
            return Ok(_infoService.deleteBlog(menuId));
        }

        [HttpPost("search")]
        public async Task<IActionResult> search([FromBody] BlogReq filter)
        {
            return Ok(_infoService.getPagin(filter));
        }
    }
}
