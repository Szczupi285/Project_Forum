using System;
using System.Collections.Generic;

namespace Project_Forum.Models.Entities;

public partial class Tag
{
    public string TagName { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
