namespace Project3.Entity.Dto
{
    public class VAnswerQuestionRes
    {
        public long Id { get; set; }

        public long? QuestionId { get; set; }

        public string? AnswerQuestion { get; set; }

        public int? Score { get; set; }

        public int? IsDelete { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
