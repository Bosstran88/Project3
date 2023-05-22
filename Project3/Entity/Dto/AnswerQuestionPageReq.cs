namespace Project3.Entity.Dto
{
    public class AnswerQuestionPageReq
    {
        public string? nameAnswer { get; set; }
        public long? questionId { get; set; }
        public int? pageSize { get; set; }  
        public int? pageNumber { get; set; }
    }
}
