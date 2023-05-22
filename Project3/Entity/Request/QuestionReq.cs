namespace Project3.Entity.Request
{
    public class QuestionReq
    {
        public long? ExamId { get; set; }
        public string? NameQuestion { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? pageSize { get; set; }
        public int? pageNumber { get; set; }
    }
}
