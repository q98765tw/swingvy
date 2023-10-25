using swingvy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using swingvy.Repositories;
using swingvy.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<swingvyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestBananaContext")));
//builder.Services.AddDbContext<swingvyContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

// Add services to the container.

// Add CalendarService registration ¦æ¨Æ¾ä
builder.Services.AddScoped<CalendarPageRepository>();
builder.Services.AddScoped<EmployeeListRepository>();
builder.Services.AddScoped<HomeRepository>();
builder.Services.AddScoped<MemberCenterService>();
builder.Services.AddScoped<MemberCenterRepository>();
builder.Services.AddControllersWithViews();
//Service
builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<LoginService>();
//Repository
builder.Services.AddScoped<IMemberRepository,MemberRepository>();
builder.Services.AddScoped<MemberDataRepository>();
builder.Services.AddScoped<ICalendarRepository,CalendarRepository>();
builder.Services.AddScoped<WorktimeRepository>();
builder.Services.AddScoped<LeaveOrderRepository>();
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
