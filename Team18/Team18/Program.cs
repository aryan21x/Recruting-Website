using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Team18.Data;
using Team18.Models;
//using Team18.Data.SeedData;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Team18ContextConnection") ?? throw new InvalidOperationException("Connection string 'Team18ContextConnection' not found.");

builder.Services.AddDbContext<Team18Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<Team18Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Team18Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Team18Context") ?? throw new InvalidOperationException("Connection string 'Team18Context' not found.")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
