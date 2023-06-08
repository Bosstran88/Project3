using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("update"), Authorize]
        public async Task<IActionResult> createOrUpdate([FromBody] UpdateInformationStudent? menuReq)
        {
            return Ok(_infoService.UpdateInfo(menuReq));
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> getById(long Id)
        {
            return Ok(_infoService.getOne(Id));
        }

        [HttpDelete("deleteById/{id}") , Authorize]
        public async Task<IActionResult> deleteById(long id)
        {
            return Ok(_infoService.deleteAcount(id));
        }

        [HttpPost("search") , Authorize]
        public async Task<IActionResult> search([FromBody] InfomationStudentReq filter)
        {
            return Ok(_infoService.getPagin(filter));
        }
    }
}
