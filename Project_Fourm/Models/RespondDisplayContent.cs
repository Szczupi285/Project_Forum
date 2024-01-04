namespace Project_Forum.Models
{
    public class RespondDisplayContent 
    {
        public int RespondId;

        public int PostId;
        public string Username { get; set; } = "notfound";
        public string Content { get; set; } = "notfound";
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int UpvotesCount { get; set; } = 0;

        public RespondDisplayContent(string username, string content, DateTime creationDate, int upvoteCount, int respondId, int postId)
        {
            Username = username;
            Content = content;
            CreationDate = creationDate;
            UpvotesCount = upvoteCount;
            RespondId = respondId;
            PostId = postId;
        }
    }
}
