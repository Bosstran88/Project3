using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class User
{
    public long Id { get; set; }

    public string? UserName { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public byte[]? PasswordHash { get; set; }

    public int? IsDelete { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } 

    public virtual InformationStudent? InformationStudent { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }

}
