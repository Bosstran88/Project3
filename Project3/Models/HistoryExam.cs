using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class HistoryExam
{
    public long Id { get; set; }

    public long? ExamId { get; set; }

    public long? InformationStudentsId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AnswerQuestionChose> AnswerQuestionChoses { get; set; }

    public virtual InformationStudent? Exam { get; set; }
}
