namespace Project3.Entity.Request
{
    public class AddCourseReq
    {
        public long? Id { get; set; }

        public string? CoursesName { get; set; }

        public int? TotalTime { get; set; }

        public long? CreatedId { get; set; }
    }
}
