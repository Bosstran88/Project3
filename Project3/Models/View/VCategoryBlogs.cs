using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models.View
{
    [Table("V_CategoryBlogs")]
    public class VCategoryBlogs
    {
        public long Id { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
