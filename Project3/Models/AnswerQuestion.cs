using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models;
public partial class AnswerQuestion
{
    public long Id { get; set; }

    public long? QuestionId { get; set; }

    public string? Answer { get; set; }

    public int? Score { get; set; }

    public int? IsDelete { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Question? Question { get; set; }
}
