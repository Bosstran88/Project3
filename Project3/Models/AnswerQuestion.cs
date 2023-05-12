namespace Project3.Models
{
    public partial class AnswerQuestion
    {
        public long Id { get; set; }
        public long? QuestionId { get; set; }
        public string? AnswerQuestions { get; set; }
        public int? Score { get; set; }
        public virtual Question? Questions { get; set; }
    }
}
