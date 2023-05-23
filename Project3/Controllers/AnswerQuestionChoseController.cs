using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerQuestionChoseController : ControllerBase
    {
        AnswerQuestionChoseService _service;

        public AnswerQuestionChoseController(AnswerQuestionChoseService service)
        {
            _service = service;
        }

        [HttpPost("/createAnswer")]
        public IActionResult creat([FromBody] RequestAnswerReq req)
        {
            return Ok(_service.create(req));
        }

        [HttpGet("/getAnswerById/{id}")]
        public IActionResult getOne(long id) 
        {
            return Ok(_service.getOne(id));
        }
    }
}
