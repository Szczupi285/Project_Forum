﻿using Microsoft.AspNetCore.Identity;

namespace Project_Forum.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? AvatarFilePath { get; set; }

        public virtual ICollection<PostUpvote> PostUpvotes { get; set; } = new List<PostUpvote>();

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public virtual ICollection<RespondUpvote> RespondUpvotes { get; set; } = new List<RespondUpvote>();

        public virtual ICollection<Respond> Responds { get; set; } = new List<Respond>();

    }
}