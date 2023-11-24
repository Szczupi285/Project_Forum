namespace Project_Forum.Models
{
    public class FilterPosts
    {
        public string SelectedHourFilter { get; set; } = "6h";

        public string? FilterByTags { get; set; } = null;
    }
}
