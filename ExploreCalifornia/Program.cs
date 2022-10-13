using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var blogConnectionString = builder.Configuration.GetConnectionString("BlogDataContext");
var identityConnectionString = builder.Configuration.GetConnectionString("IdentityDataContext");

builder.Services.AddDbContext<BlogDataContext>(options => options.UseSqlServer(blogConnectionString));
builder.Services.AddDbContext<IdentityDataContext>(options => options.UseSqlServer(identityConnectionString));
builder.Services.AddTransient<FormattingService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityDataContext>();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();

app.UseExceptionHandler("/error.html");

app.UseAuthentication();

app.UseMvc(routes =>
{
    routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
});

app.UseFileServer();
app.Run();
