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
        HistoryExamService _service;
        public HistoryExamController() { }


        [HttpPost("/create")]
        public IActionResult creat([FromBody] AddHistoryExamReq historyExamReq)
        {
            return Ok(_service.create(historyExamReq));
        }

        [HttpGet("/getById/{id}")]
        public IActionResult getOne(long id)
        {
            return Ok(_service.getOne(id));
        }
    }
}
