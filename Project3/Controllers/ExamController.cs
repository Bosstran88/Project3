using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost("createOrUpdate")]
        public async Task<IActionResult> createOrUpdate([FromBody] AddExamReq? menuReq)
        {
            return Ok(_examService.createOrUpdate(menuReq));
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> getById(long id)
        {
            return Ok(_examService.getOne(id));
        }

        [HttpDelete("deleteById/{menuId}")]
        public async Task<IActionResult> deleteById(long menuId)
        {
            return Ok(_examService.deleteExam(menuId));
        }

        [HttpPost("search")]
        public async Task<IActionResult> search([FromBody] ExamReq filter)
        {
            return Ok();
            //return Ok(_examService.getPagin(filter));

        }

    }
}
