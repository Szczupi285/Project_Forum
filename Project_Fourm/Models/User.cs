using System;
using System.Collections.Generic;

namespace Project_Fourm.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Passwd { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string? AvatarFilePath { get; set; }

    public bool IsAdmin { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Post> PostsNavigation { get; set; } = new List<Post>();

    public virtual ICollection<Respond> Responds { get; set; } = new List<Respond>();
}
