namespace Project3.Entity.Dto
{
    public class VAnswerQuestionOne
    {
        public long Id { get; set; }

        public long? QuestionId { get; set; }

        public string? AnswerQuestion1 { get; set; }

        public int? Score { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }
    }
}
