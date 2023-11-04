using System;
using System.Collections.Generic;

namespace Project_Forum.Models;

public partial class PostTag
{
    public int? PostId { get; set; }

    public string? TagName { get; set; }

    public virtual Post? Post { get; set; }

    public virtual Tag? TagNameNavigation { get; set; }
}
