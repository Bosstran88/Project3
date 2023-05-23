using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryBlogController : ControllerBase
    {
        ICategoryBlogService _categoryBlogService;

        public CategoryBlogController(ICategoryBlogService categoryBlogService)
        {
            _categoryBlogService = categoryBlogService;
        }

        [HttpPost("createOrUpdate")]
        public async Task<IActionResult> createOrUpdate([FromBody] AddCategoryBlog? categoryBlog)
        {
            return Ok(_categoryBlogService.createOrUpdate(categoryBlog));
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> getById(long Id)
        {
            return Ok(_categoryBlogService.getOne(Id));
        }

        [HttpDelete("deleteById/{Id}")]
        public async Task<IActionResult> deleteById(long Id)
        {
            return Ok(_categoryBlogService.deleteCategoy(Id));
        }

        [HttpPost("search")]
        public async Task<IActionResult> search([FromBody] CategoryBlogReq filter)
        {
            return Ok(_categoryBlogService.search(filter));
        }
    }
}
