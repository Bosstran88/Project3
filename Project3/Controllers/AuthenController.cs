using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Services;
using Project3.Utils;
using System.Security.Claims;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        IUserService userService;
        public AuthenController(IUserService _userService) 
        {
            userService = _userService;
        }
        [HttpPost("/registerUser")]
        public IActionResult RegisterUser([FromBody] RegisterReq? registerUser)
        {
            return Ok(userService.register(registerUser));
        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] LoginReq login)
        {
            return Ok(userService.login(login));
        }

        [HttpGet("/current") , Authorize]
        public IActionResult getInfo() 
        {
            long userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string userName = User.FindFirst(ClaimTypes.Name)?.Value.ToString();
            string roleName = User.FindFirst(ClaimTypes.Role)?.Value.ToString();
            var data = new AuthenReq
            {
                Id = userId,
                UserName = userName,
                RoleName = roleName
            };
            return Ok(userService.getInfo(data));
        }

        [HttpPost("search")]
        public IActionResult search([FromBody] UserPaginReq res)
        {
            return Ok(userService.search(res));
        }

        [HttpGet("detail/{id}")]
        public IActionResult getOne(long id)
        {
            return Ok(userService.getOne(id));
        }
    }
}
