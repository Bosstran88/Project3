using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerQuestionController : ControllerBase
    {
        IAnswerQuestionService _answerQuestionService;

        public AnswerQuestionController(IAnswerQuestionService answerQuestionService)
        {
            _answerQuestionService = answerQuestionService;
        }

        [HttpPost("createOrUpdate")]
        public async Task<IActionResult> createOrUpdate([FromBody] AddAnswerQuestionReq? menuReq)
        {
            return Ok(_answerQuestionService.createOrUpdate(menuReq));
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> getById(long id)
        {
            return Ok(_answerQuestionService.getOne(id));
        }

        [HttpDelete("deleteById/{menuId}")]
        public async Task<IActionResult> deleteById(long menuId)
        {
            return Ok(_answerQuestionService.deleteAnswerQuestion(menuId));
        }

        [HttpPost("search")]
        public async Task<IActionResult> search([FromBody] AnswerQuestionReq filter)
        {
            //return Ok(_answerQuestionService.getPagin(filter));
            return Ok();
        }
    }
}
