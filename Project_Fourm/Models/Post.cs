using Project_Forum.Models;
using System;
using System.Collections.Generic;

namespace Project_
    .Models;

public partial class Post
{
    public int PostId { get; set; }

    public int UserId { get; set; }

    public string PostContent { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
