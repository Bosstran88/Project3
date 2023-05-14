using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class Exam
{
    public long Id { get; set; }

    public string? NameExam { get; set; }

    public int? LimitTime { get; set; }

    public int? IsDelete { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Question> Questions { get; set; } 
}
