namespace Project_Forum.Services.Upvoting
{
    public interface IUpvotingService
    {
        Task ManageUpvoteAsync(string userId, int postId);
        Task ManageRespondUpvoteAsync(string userId, int respondId);
    }
}
