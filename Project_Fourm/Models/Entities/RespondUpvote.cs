using System;
using System.Collections.Generic;

namespace Project_Forum.Models.Entities;

public partial class RespondUpvote
{
    public int RespondUpvotesId { get; set; }

    public string UserId { get; set; } = null!;

    public int RespondId { get; set; }

    public virtual Respond Respond { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
}
