using Business.Abstract;
using Business.Concrete;
using Business.JobSchedulerManagers;
using Business.Middleware;
using Business.NotificationManagers;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Spi;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLog4Net("log4net.config");



// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();






builder.Services.AddScoped<ITargetAppRepository, EfTargetAppRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITargetAppService, TargetAppService>();

builder.Services.AddTransient<INotificationManager, NotificationManager>();

builder.Services.AddTransient<INotificationSender, EmailNotificationSender>();
builder.Services.AddTransient<IJobSchedulerManager, JobSchedulerManager>();

var jobManager=builder.Services.BuildServiceProvider().GetService<IJobSchedulerManager>();


builder.Services.AddQuartz(q =>
{
    
    jobManager.Start();

});

builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);

var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
