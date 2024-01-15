using Project_Forum.Models.Entities;

namespace Project_Forum.Models
{
    public class PostDisplayContent 
    {
        public string UserId;

        public int PostId;
        public string Username { get; set; } = "notfound";
        public string Content { get; set; } = "notfound";
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int UpvotesCount { get; set; } = 0;

        

        public PostDisplayContent(string username, string content, DateTime creationDate, int upvoteCount, int postId, string userId)
        {
            Username = username;
            Content = content;
            CreationDate = creationDate;
            UpvotesCount = upvoteCount;
            PostId = postId;
            UserId = userId;
        }
    }
}
