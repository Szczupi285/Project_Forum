using System;
using System.Collections.Generic;

namespace Project_Forum.Models;

public partial class PostUpvote
{
    public int RespondUpvotesId { get; set; }

    public string UserId { get; set; } = null!;

    public int PostId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
}
