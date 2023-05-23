using System.Globalization;

namespace Project3.Models
{
    public class Address
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Addresses { get; set; }
        public int? IsDelete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
