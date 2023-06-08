namespace Project3.Entity.Dto
{
    public class DetailSubject
    {
        public long Id { get; set; }

        public string? SubjectName { get; set; }

        public int? TotalTime { get; set; }

        public decimal? Price { get; set; }

        public int? IsDelete { get; set; }

        public long? CoursesId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }
    }
}
