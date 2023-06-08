using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryExamController : ControllerBase
    {
        IHistoryExamService _service;
        public HistoryExamController(IHistoryExamService service) 
        {
            _service = service;
        }


        [HttpPost("create"), Authorize]
        public IActionResult create([FromBody] AddHistoryExamReq historyExamReq)
        {
            return Ok(_service.create(historyExamReq));
        }

        [HttpGet("getById/{id}")]
        public IActionResult getOne(long id)
        {
            return Ok(_service.getOne(id));
        }

        [HttpPost("update/{historyExamId}"), Authorize]
        public IActionResult update(long historyExamId)
        {
            return Ok(_service.updateExam(historyExamId));
        }

        [HttpGet("getResult/{examId}/{idCardStudent}")]
        public IActionResult getResult(long examId , string idCardStudent)
        {
            return Ok(_service.getResultExam(idCardStudent,examId));
        }


    }
}
