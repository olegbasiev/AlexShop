using AlexShopDb;
using AlexShopDb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection"); //"DefailtConnection"


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<DatabaseContext>();


var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
	var services = serviceScope.ServiceProvider;
	var userManager = services.GetRequiredService<UserManager<User>>();
	var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
	IdentityInitializer.Initialize(userManager, rolesManager);
}

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
