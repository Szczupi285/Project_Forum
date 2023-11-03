using Project_Fourm.Models;

namespace Project_Fourm.Services
{
    public interface IRegisterService
    {
        Task RegisterUser(ForumProjectContext context, string username, string password, string email, DateTime date);
    }
}
