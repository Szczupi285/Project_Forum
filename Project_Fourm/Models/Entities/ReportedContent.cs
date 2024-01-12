using System;
using System.Collections.Generic;

namespace Project_Forum.Models.Entities;

public partial class ReportedContent
{
    public int ReportId { get; set; }

    public string ReportedUserId { get; set; } = null!;

    public string SubmitterId { get; set; } = null!;

    public string? ModeratorId { get; set; }

    public int ContentId { get; set; }

    public string ContentType { get; set; } = null!;

    public bool IsResolved { get; set; }

    public string? Reason { get; set; }

    public string Content { get; set; } = null!;

    public string? Resolution { get; set; }

    public DateTime ReportDate { get; set; }

    public DateTime? ResolveDate { get; set; }

    public virtual ApplicationUser? Moderator { get; set; }

    public virtual Warning? Warning { get; set; }
}
