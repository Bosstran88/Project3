namespace Project3.Models
{
    public partial class AnswerQuestionChose
    {
        public long Id { get; set; }
        public long? QuestionId { get; set; }
        public string? Answerchose { get; set; }
        public long? HistoryExamId { get; set; }
        public virtual HistoryExam? HistoryExams { get; set; }
    }
}
