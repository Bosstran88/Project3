using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class Course
{
    public long Id { get; set; }

    public string? CoursesName { get; set; }

    public int? TotalTime { get; set; }

    public int? IsSale { get; set; }

    public string? Level { get; set; }

    public int? IsDelete { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Subject> Subjects { get; set; }
}
