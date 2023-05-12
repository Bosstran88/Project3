namespace Project3.Entity.Request
{
    public class AnswerQuestionReq
    {
        public long? QuestionId { get; set; }
        public string? AnswerQuestions { get; set; }
        public int? Score { get; set; }
    }
}
