using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using wep.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions( options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddDbContext<ServisContext>(options=>
{
    var connectoinString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectoinString);
});

builder.Services.AddDefaultIdentity<UserDetails>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ServisContext>();
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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
