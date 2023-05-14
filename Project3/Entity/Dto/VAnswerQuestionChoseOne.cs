namespace Project3.Entity.Dto
{
    public class VAnswerQuestionChoseOne
    {
        public long? Id { get; set; }

        public long? QuestionId { get; set; }

        public string? AnswerChose { get; set; }

        public long? HistoryExamId { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
