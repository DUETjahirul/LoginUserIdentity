using LoginUserIdentity.Data; // Importing the ApplicationContext class mean folder location
using LoginUserIdentity.Repository.Interface;
using LoginUserIdentity.Repository.Service;
using Microsoft.AspNetCore.Identity; // Importing necessary namespaces for Identity //2
using Microsoft.EntityFrameworkCore; // Importing necessary namespaces for DbContext //1
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//1
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
 }); 
//2
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders(); 

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Redirect to the login page if not authenticated
    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect to access denied page if authorization fails
    options.LogoutPath = "/Account/Logout"; // Redirect to the logout page
    options.Cookie.Name= "LoginUserIdentityAppCookie"; // Set the cookie name
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true; // Enable sliding expiration
});
builder.Services.AddTransient<IEmailSender, EmailSender>(); // Register the IEmailSender service with transient lifetime
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
