using Microsoft.AspNetCore.Identity;
using Project_Forum.Models.Entities;

namespace Project_Forum.Services.Observe
{
    public class ObserveService : IObserveService
    {

        private readonly ForumProjectContext Context;
        private readonly UserManager<ApplicationUser> UserManager;

        public ObserveService(ForumProjectContext context, UserManager<ApplicationUser> userManager)
        {
            Context = context;
            UserManager = userManager;
        }


        public async Task<bool> HandleTagObservation(string tagName, string userId)
        {
            bool AlreadyObserved = false;
            var user = await Context.AspNetUsers.FindAsync(userId);
            var tag = await Context.Tags.FindAsync(tagName);


            if (user is null || tag is null)
                return false;
            else
            {
                AlreadyObserved = Context.AspNetUsers.Where(u => u.Id == userId).
                               SelectMany(u => u.TagNames).Contains(tag);

                if (AlreadyObserved is false)
                    user.TagNames.Add(tag);
                else
                {
                    // since both of the collections in many to many relationship need to be loaded 
                    // before removing item we need to explicitly load related entities
                    Context.Entry(user).Collection("TagNames").Load();
                    user.TagNames.Remove(tag);
                }

                await Context.SaveChangesAsync();
                return true;
            }

        }

        public async Task<bool> IsTagObserved(string tagName, string userId)
        {

            var user = await Context.AspNetUsers.FindAsync(userId);
            var tag = await Context.Tags.FindAsync(tagName);
            if (user is null || tag is null)
                return false;
            else
            {
                return Context.AspNetUsers.Where(u => u.Id == userId).
                                              SelectMany(u => u.TagNames).Contains(tag);
            }

        }
    }
}
