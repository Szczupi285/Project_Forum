using Project_Forum.Models.Entities;

namespace Project_Forum.Models
{
    public class PostCompositeModel
    {
        public string? CurrentUserId;

        public string? visibility;
        public Post PostModel { get; set; } = new Post();

        public FilterPosts FilterPostsModel { get; set; } = new FilterPosts();

        public ApplicationUser UserModel { get; set; } = new ApplicationUser();

        public PostUpvote PostUpvoteModel { get; set; } = new PostUpvote();

        public Respond RespondModel { get; set; } = new Respond();

        public RespondUpvote RespondUpvoteModel { get; set; } = new RespondUpvote();

        public HashSet<(PostDisplayContent, List<RespondDisplayContent>)> PostDisplayContents { get; set; } = new HashSet<(PostDisplayContent, List<RespondDisplayContent>)>();

        
    }
}