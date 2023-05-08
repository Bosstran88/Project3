namespace Project3.Entity.Request
{
    public class SubjectReq
    {
        public string? subjectName { get; set; }
        public decimal? price { get; set; }
        public long? courseId { get; set; }
        public int? pageSize { get; set; }
        public int? pageNumber { get; set; } = 0;
    }
}
