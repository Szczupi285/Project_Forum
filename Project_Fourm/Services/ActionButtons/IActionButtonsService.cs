namespace Project_Forum.Services.ActionButtons
{
    public interface IActionButtonsService
    {
        Task<bool> RemovePost(int postId);

        Task<bool> RemoveRespond(int respondId);

        Task<bool> ReportContent(int contentId, string submitterId, string reason, string contentType);

        Task<bool> EditContent(int contentId, string newContent, string contentType);

    }
}
