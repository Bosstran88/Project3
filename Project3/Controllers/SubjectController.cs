using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        ISubjectService subjectService;
        public SubjectController(ISubjectService _subjectService) 
        {
            subjectService = _subjectService;
        }

        [HttpPost("createOrUpdate") , Authorize]
        public async Task<IActionResult> createOrUpdate([FromBody] AddSubjectReq? menuReq)
        {
            return Ok(subjectService.createOrUpdate(menuReq));
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> getById(long Id)
        {
            return Ok(subjectService.getOne(Id));
        }

        [HttpDelete("deleteById/{id}") , Authorize]
        public async Task<IActionResult> deleteById(long id)
        {
            return Ok(subjectService.deleteSubject(id));
        }

        [HttpPost("search")]
        public async Task<IActionResult> search([FromBody] SubjectReq filter)
        {
            return Ok(subjectService.getPagin(filter));
        }
    }
}
