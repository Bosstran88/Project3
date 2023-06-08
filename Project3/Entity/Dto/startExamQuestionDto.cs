namespace Project3.Entity.Dto
{
    public class startExamQuestionDto
    {
        public long questionId { get; set; }
        public string questionName { get; set; }
        public List<VAnswerQuestionOne> answers { get; set; }

    }
}
