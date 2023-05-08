namespace Project3.Entity.Request
{
    public class AddSubjectReq
    {
        public long? Id { get; set; }

        public string? SubjectName { get; set; }

        public int? TotalTime { get; set; }

        public decimal? Price { get; set; }

        public long? CoursesId { get; set; }

    }
}
