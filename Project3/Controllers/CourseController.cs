using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        ICourseService _svc;
        public CourseController(ICourseService svc) 
        {
            _svc = svc;
        }

        [HttpPost("createOrUpdate"), Authorize]
        public async Task<IActionResult> createOrUpdate([FromBody] AddCourseReq addCourseReq)
        {
            return Ok(_svc.createOrUpdate(addCourseReq));
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> getById(long id)
        {
            return Ok(_svc.getOne(id));
        }

        [HttpDelete("deleteById/{id}"), Authorize]
        public async Task<IActionResult> deleteById(long id)
        {
            return Ok(_svc.deleteCourse(id));
        }

        [HttpPost("search")]
        public async Task<IActionResult> search([FromBody] CourseSearchReq filter)
        {
            return Ok(_svc.pagination(filter));
        }


    }
}
