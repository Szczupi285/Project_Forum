        using Project_Fourm.Models;
        using System.CodeDom;

        namespace Project_Fourm.Services
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
