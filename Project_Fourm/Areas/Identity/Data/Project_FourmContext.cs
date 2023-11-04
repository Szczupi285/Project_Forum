using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Forum.Models;

namespace Project_Forum.Data;

public class Project_ForumContext : IdentityDbContext<ApplicationUser>
{

    public Project_ForumContext()
    {
    }

    public Project_ForumContext(DbContextOptions<Project_ForumContext> options)
        : base(options)
    {
    }
    protected Project_ForumContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-EKI8377;Database=Forum_Project;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
