using Project_Forum.Models;

namespace Project_Forum.Services.RetriveContent
{
    public interface IRetriveContentService
    {
        public Task<List<PostDisplayContent>> RetrivePostContentAsync(int numberOfPosts, DateTime dateTime);
        public Task<List<PostDisplayContent>> RetrivePostContentAsync(int numberOfPosts);

        Task<List<RespondDisplayContent>> RetriveRespondContentAsync(int postId);

        Task<List<PostDisplayContent>> RetrivePostsByTag(int numberOfPosts, DateTime showPostSince, string tag);

        Task<List<PostDisplayContent>> RetriveFeed(int numberOfPosts, DateTime showPostSince, string userId);
    }
}
