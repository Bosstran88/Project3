namespace Project3.Entity.Request
{
    public class AnswerQuestionReq
    {
        public string? AnswerQuestions { get; set; }
        public int? Score { get; set; }
        public int? pageSize { get; set; }
        public int? pageNumber {get; set; }
    }
}
