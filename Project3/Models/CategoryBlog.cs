using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class CategoryBlog
{
    public long Id { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public int? IsDelete { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; }
}
