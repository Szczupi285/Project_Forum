using Project_Forum.Models;

namespace Project_Forum.Services
{
    public interface IRegisterService
    {
        Task RegisterUser(ForumProjectContext context, string username, string password, string email, DateTime date);
    }
}
