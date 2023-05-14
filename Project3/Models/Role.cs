using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class Role
{
    public long Id { get; set; }

    public string? NameRole { get; set; }

    public int? IsDelete { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }
}
