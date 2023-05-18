namespace Project3.Entity.Request
{
    public class ExamReq
    {
        public string? NameExam { get; set; }
        public int? LimitTime { get; set; }
        public int? pageSize { get; set; }
        public int? pageNumber { get; set; } = 0;
    }
}
