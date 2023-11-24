namespace Project_Forum.Models
{
    public class PostCompositeModel
    {
        public Post PostModel { get; set; } = new Post();

        public FilterPosts FilterPostsModel { get; set; } = new FilterPosts();

        public ApplicationUser UserModel { get; set; } = new ApplicationUser(); 

        public PostTag PostTagModel { get; set; } = new PostTag();

        public PostUpvote PostUpvoteModel { get; set; } = new PostUpvote();

        public Respond RespondModel { get; set; } = new Respond();

        public RespondUpvote RespondUpvoteModel { get; set; } = new RespondUpvote();
    }
}
