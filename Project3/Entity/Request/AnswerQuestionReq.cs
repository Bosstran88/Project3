namespace Project3.Entity.Request
{
    public class AnswerQuestionReq
    {
        public long? QuestionId { get; set; }
        public string? AnswerQuestion1 { get; set; }
        public int? Score { get; set; }
        public int? pageSize { get; set; }
        public int? pageNumber { get; set; } = 0;
    }
}
