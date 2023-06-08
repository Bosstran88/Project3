﻿namespace Project3.Models;

public partial class HistoryExam
{
    public long Id { get; set; }

    public long? InformationStudentId { get; set; }

    public long? ExamId { get; set; }

    public int? Score { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AnswerQuestionChose> AnswerQuestionChoses { get; set; }

    public virtual InformationStudent? Exam { get; set; }

}
