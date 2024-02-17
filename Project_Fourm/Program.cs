using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Project_Forum.Models;
using Microsoft.AspNetCore.Identity;
using Project_Forum.Data;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Project_Forum.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Project_Forum.Services;
using Project_Forum.Services.Login;
using Project_Forum.Services.Register;
using Project_Forum.Services.PostCreation;
using Project_Forum.Services.Moderator;
using Project_Forum.Services.ActionButtons;
using Project_Forum.Services.Observe;
using Project_Forum.Services.Upvoting;
using Project_Forum.Services.RetriveContent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ForumProjectContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ForumProjectContext>().AddDefaultTokenProviders();
   
    

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IPostCreationService, PostCreation>();
builder.Services.AddScoped<IModeratorService, ModeratorService>();
builder.Services.AddScoped<IActionButtonsService, ActionButtonService>();
builder.Services.AddScoped<IObserveService, ObserveService>();
builder.Services.AddScoped<IUpvotingService, UpvotingService>();
builder.Services.AddScoped<IRetriveContentService, RetriveContentService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Forum/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
name: "default",
pattern: "{controller=Forum}/{action=LogOut}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Moderator", "User" };

    foreach(var role in roles)
    {
         if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

app.Run();
