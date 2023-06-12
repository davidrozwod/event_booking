using DotNetEd.CoreAdmin;
using event_booking.Data;
using event_booking.Data.Migrations;
using event_booking.Models;
using event_booking.Services;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace event_booking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

// start of services//
            //Razor pages
            builder.Services.AddRazorPages();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            // DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            // Database Developer Page Exception Filter
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //Identity
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()  
                .AddEntityFrameworkStores<ApplicationDbContext>();
            // Controllers
            builder.Services.AddControllersWithViews();

            // Email sender
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            // AuthMessageSenderOptions
            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

            // Session state service for storing user data in session variables (e.g. shopping cart)
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5); // Set the session timeout
            });

            // end of services//

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSession();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Razor pages
            app.MapRazorPages();
            // Controller routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}