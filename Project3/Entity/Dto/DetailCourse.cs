using Project3.Models;

namespace Project3.Entity.Dto
{
    public class DetailCourse
    {
        public long Id { get; set; }

        public string? CoursesName { get; set; }

        public int? TotalTime { get; set; }

        public int? IsSale { get; set; }

        public string? Level { get; set; }

        public int? IsDelete { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
