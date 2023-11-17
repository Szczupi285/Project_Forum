using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Project_Forum.Models;

namespace Project_Forum.Services
{
    public interface IPostService
    {
        Task AddPostAsync();

        Task AddTagsAsync();

        Task AddUpvoteAsync();
    }
}
