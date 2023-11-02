using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Project_Fourm.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ForumProjectContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Forum}/{action=Index}/{id?}");

app.Run();
