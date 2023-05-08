using System.ComponentModel.DataAnnotations;

namespace Project3.Entity.Dto
{
    public class LoginReq
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
