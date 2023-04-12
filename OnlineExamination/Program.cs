using Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineExamination.Abstractions.Contract;
using OnlineExamination.Models;
using OnlineExamination.Repositorys.Implementation;
using OnlineExamination.Services;
using Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json",
        optional: true,
        reloadOnChange: true);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OnlineExaminationDBContext>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
builder.Services.AddAuthentication("OnlineExamCookiesAuth").AddCookie("OnlineExamCookiesAuth", option => {
    option.Cookie.Name = "OnlineExamCookiesAuth";
    option.LoginPath = "/Account/Login";
    option.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddAuthorization(option => {
    option.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
});

builder.Services.AddAuthorization(option => {
    option.AddPolicy("JobSeekerOnly", policy => policy.RequireClaim("JobSeeker"));
});
builder.Services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
builder.Services.AddSession(option =>
{ option.IdleTimeout = TimeSpan.FromSeconds(3600); });
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IJobSeekerService, JobSeekerService>();
builder.Services.AddTransient<ILoginRepo, LoginRepo>();
builder.Services.AddTransient<IAdminRepo, AdminRepo>();
builder.Services.AddTransient<IJobSeeker, JobSeekerRepo>();

builder.Services.AddRazorPages();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}



builder.Services.AddMvc();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
