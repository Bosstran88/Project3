using Microsoft.AspNetCore.Authorization;
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
        IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("createOrUpdate"), Authorize]
        public async Task<IActionResult> createorUpdate([FromBody] AddQuestionReq? menuReq)
        {
            return Ok(_questionService.createOrUpdate(menuReq));
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> getById(long id)
        {
            return Ok(_questionService.getOne(id));
        }

        [HttpDelete("deleteById/{menuId}")]
        public async Task<IActionResult> deletebyId(long menuId)
        {
            return Ok(_questionService.deleteQuestion(menuId));
        }

        [HttpGet("list/{id}")]
        public IActionResult getList(long id)
        {
            return Ok(_questionService.getListByExamId(id));
        }

        [HttpGet("startExam/{id}")]
        public IActionResult startExam(long id)
        {
            return Ok(_questionService.startExam(id));
        }

        [HttpPost("search")]
        public async Task<IActionResult> search([FromBody] QuestionReq filter)
        {
            return Ok(_questionService.pagination(filter));
        }
    }
}
