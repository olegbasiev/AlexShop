using Microsoft.Extensions.DependencyInjection;
using AlexShop.Models;
using Microsoft.EntityFrameworkCore;
using DBOnlineShop;



var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("online_shop"); //"DefailtConnection"


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
builder.Services.AddTransient<IServiceRepository, ServicesDbRepository>();


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

app.Run();

builder.Services.AddControllersWithViews();


app.MapDefaultControllerRoute();
app.Run();
