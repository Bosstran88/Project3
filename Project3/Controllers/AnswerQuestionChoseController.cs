using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerQuestionChoseController : ControllerBase
    {
        IAnswerQuestionChoseService _service;

        public AnswerQuestionChoseController(IAnswerQuestionChoseService service)
        {
            _service = service;
        }

        [HttpPost("/createAnswer") , Authorize]
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
