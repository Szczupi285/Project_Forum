        using Project_.Models;
using Project_Forum.Models;
using System.CodeDom;

        namespace Project_Forum.Services
        {
            public class RegisterService : IRegisterService
            {
                public async Task RegisterUser(ForumProjectContext context, string username, string password, string email, DateTime date)
                {
                        User user = new User
                        {
                            Username = username,
                            Passwd = password,
                            Email = email,
                            DateOfBirth = date,
                            IsAdmin = false
                        };

                        await context.Users.AddAsync(user);
                        await context.SaveChangesAsync();


                }
            }
        }
