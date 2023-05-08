using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Dto;
using Project3.Services;

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
        public IActionResult RegisterUser([FromBody] RegisterReq registerUser)
        {
            return Ok(userService.register(registerUser));
        }

        [HttpPost("/loginUser")]
        public IActionResult Login([FromBody] LoginReq login)
        {
            return Ok(userService.login(login));
        }
    }
}
