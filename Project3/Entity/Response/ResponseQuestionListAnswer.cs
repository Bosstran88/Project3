using Project3.Entity.Dto;

namespace Project3.Entity.Response
{
    public class ResponseQuestionListAnswer
    {
        public long? questionId { get; set; }
        public string? nameQuestion { get; set; }
        public List<VAnswerQuestionDto> listAnswer { get; set; } 
    }
}
