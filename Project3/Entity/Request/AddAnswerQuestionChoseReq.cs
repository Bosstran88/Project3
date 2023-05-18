namespace Project3.Entity.Request
{
    public class AddAnswerQuestionChoseReq
    {
        public long? Id { get; set; }

        public long? QuestionId { get; set; }

        public string? AnswerChose { get; set; }

        public long? HistoryExamId { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
