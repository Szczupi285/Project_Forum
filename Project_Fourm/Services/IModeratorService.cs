using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.Net.Http.Headers;
using Project_Forum.Models;

namespace Project_Forum.Services
{
    public interface IModeratorService
    {
        Task<List<ReportDisplayContent>> RetriveNotSolvedReports();
        Task<bool> Delete(int reportId, int contentId, string moderatorId, string contentType);
        Task<bool> Keep(int reportId, string moderatorId);
    }
}
