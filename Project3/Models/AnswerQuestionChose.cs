

namespace Project3.Models;


public partial class AnswerQuestionChose
{
    public long Id { get; set; }

    public long? QuestionId { get; set; }

    public long? AnswerChoseId { get; set; }

    public long? HistoryExamId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual HistoryExam? HistoryExam { get; set; }
}
