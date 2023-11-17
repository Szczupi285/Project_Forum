using System;
using System.Collections.Generic;

namespace Project_Forum.Models;

public partial class Post
{
    public int PostId { get; set; }

    public string UserId { get; set; } = null!;

    public string PostContent { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public virtual ICollection<PostUpvote> PostUpvotes { get; set; } = new List<PostUpvote>();

    public virtual ICollection<Respond> Responds { get; set; } = new List<Respond>();

    public virtual ApplicationUser User { get; set; } = null!;
}
