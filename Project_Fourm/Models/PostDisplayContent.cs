namespace Project_Forum.Models
{
    public class PostDisplayContent 
    {
        public string Username { get; set; } = "notfound";
        public string Content { get; set; } = "notfound";
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int UpvotesCount { get; set; } = 0;

        public PostDisplayContent(string username, string content, DateTime creationDate, int upvoteCount)
        {
            Username = username;
            Content = content;
            CreationDate = creationDate;
            UpvotesCount = upvoteCount;
        }
    }
}
