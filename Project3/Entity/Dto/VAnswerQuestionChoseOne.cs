namespace Project3.Entity.Dto
{
    public class VAnswerQuestionChoseOne
    {
        public long? Id { get; set; }

        public long? QuestionId { get; set; }

        public long? AnswerChoseId { get; set; }

        public long? HistoryExamId { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
