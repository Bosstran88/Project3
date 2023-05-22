using System.ComponentModel.DataAnnotations;

namespace Project3.Entity.Dto
{
    public class RegisterReq
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? role { get; set; }
        public string? idCardStudent { get;set; }
        public string? fullName { get; set; }
    }
}
