namespace Project3.Entity.Dto
{
    public class VCourseRes
    {
        public long Id { get; set; }

        public string? CoursesName { get; set; }

        public int? TotalTime { get; set; }

        public int? IsSale { get; set; }

        public string? Level { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }
    }
}
