namespace Project3.Entity.Dto
{
    public class VSubjectPagin
    {
        public long Id { get; set; }

        public string? SubjectName { get; set; }
        public decimal? Price { get; set; }
        public long? CoursesId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
