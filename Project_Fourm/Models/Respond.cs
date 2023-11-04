using Project_Forum.Models;
using System;
using System.Collections.Generic;

namespace Project_
    .Models;

public partial class Respond
{
    public int RespondId { get; set; }

    public string RepondContent { get; set; } = null!;

    public int UserId { get; set; }

    public int PostId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
