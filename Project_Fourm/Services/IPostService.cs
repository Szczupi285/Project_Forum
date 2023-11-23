﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Project_Forum.Models;

namespace Project_Forum.Services
{
    public interface IPostService
    {
        Task AddPostAsync(string userId, string postContent);

        Task AddTagsAsync(string postContent);

        Task AddPostTagsAsync(string postId, string tagId, string postContent);

        Task AddUpvoteAsync();
    }
}