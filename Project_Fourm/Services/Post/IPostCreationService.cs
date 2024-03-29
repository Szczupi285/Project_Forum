﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Project_Forum.Models;

namespace Project_Forum.Services.PostCreation
{
    public interface IPostCreationService
    {
        Task<int> AddPostAsync(string userId, string postContent);

        Task<HashSet<string>> AddTagsAsync(string postContent);

        Task<HashSet<(int, string)>> AddPostTagsAsync(int postId, HashSet<string> tags);

        Task AddRespondAsync(int postId, string userId, string respondContent);


    }
}
