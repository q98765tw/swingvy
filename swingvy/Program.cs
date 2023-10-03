using swingvy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using swingvy.Repositories;
using swingvy.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<swingvyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestBananaContext")));
// Add services to the container.

// Add CalendarService registration ¦æ¨Æ¾ä
builder.Services.AddScoped<CalendarService>();
builder.Services.AddScoped<EmployeeListService>();
builder.Services.AddScoped<HomeService>();
builder.Services.AddControllersWithViews();
//Service
builder.Services.AddTransient<RegisterService>();
builder.Services.AddTransient<LoginService>();
//Repository
builder.Services.AddTransient<MemberRepository>();
builder.Services.AddTransient<MemberDataRepository>();
builder.Services.AddTransient<CalendarRepository>();
builder.Services.AddTransient<WorktimeRepository>();
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
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
