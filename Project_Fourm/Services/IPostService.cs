using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Project_Forum.Models;

namespace Project_Forum.Services
{
    public interface IPostService
    {
        Task<int> AddPostAsync(string userId, string postContent);

        Task<HashSet<string>> AddTagsAsync(string postContent);

        Task<HashSet<(int, string)>> AddPostTagsAsync(int postId, HashSet<string> tags);

        public Task<List<PostDisplayContent>> RetrivePostContentAsync(int numberOfPosts, DateTime dateTime);

        Task ManageUpvoteAsync(string userId, int postId);

        Task AddRespondAsync(int postId, string userId, string respondContent);

        Task ManageRespondUpvoteAsync(string userId, int respondId);

        Task<List<RespondDisplayContent>> RetriveRespondContentAsync(int postId);

        Task<bool> RemovePost(int postId);
        
        Task<bool> RemoveRespond(int respondId);

        Task<bool> ReportContent(int postId, string submitterId, string reason, string contentType);

  

    }
}
