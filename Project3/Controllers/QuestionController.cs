using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        IQuestionService questionService;

        public QuestionController(IQuestionService _questionService)
        {
            questionService = _questionService;
        }

        [HttpPost("createOrUpdate")]
        public async Task<IActionResult> createorUpdate([FromBody] AddQuestionReq? menuReq)
        {
            return Ok(questionService.createOrUpdate(menuReq));
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> getById(long id)
        {
            return Ok(questionService.getOne(id));
        }

        [HttpDelete("deleteById/{menuId}")]
        public async Task<IActionResult> deletebyId(long menuId)
        {
            return Ok(questionService.deleteQuestion(menuId));
        }

        [HttpPost("search")]
        public async Task<IActionResult> search([FromBody] QuestionReq filter)
        {
            return Ok(questionService.getPagin(filter));
        }

        [HttpGet("getList")]
        public async Task<IActionResult> getList()
        {
            return null;
        }
    }
}
