using System;
using System.Collections.Generic;

namespace Project_Forum.Models.Entities;

public partial class Warning
{
    public int Id { get; set; }

    public byte WarningType { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string? AdditionalNotes { get; set; }

    public virtual ReportedContent IdNavigation { get; set; } = null!;
}
