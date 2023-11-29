using System;
using System.Collections.Generic;

namespace Project_Forum.Models.Entities;

public partial class Respond
{
    public int RespondId { get; set; }

    public string RepondContent { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int PostId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual ICollection<RespondUpvote> RespondUpvotes { get; set; } = new List<RespondUpvote>();

    public virtual ApplicationUser User { get; set; } = null!;
}
