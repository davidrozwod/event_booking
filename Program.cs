using DotNetEd.CoreAdmin;
using event_booking.Data;
using event_booking.Data.Migrations;
using event_booking.Models;
using event_booking.Services;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;


namespace event_booking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()  
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
            builder.Services.AddCoreAdmin("Promoter");
            builder.Services.AddCoreAdmin(new CoreAdminOptions() 
            { IgnoreEntityTypes = new List<Type>() { 
        
              typeof(IdentityUser),
              typeof(IdentityRole),
              typeof(UserRole),
              typeof(IdentityUserRole<string>),
              typeof(IdentityUserClaim<string>),
              typeof(IdentityUserLogin<string>),
              typeof(IdentityUserToken<string>) } 
            
            });


            var app = builder.Build();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            app.UseCoreAdminCustomTitle("Promoter Area");
            app.UseCoreAdminCustomUrl("promoterarea");

            app.Run();
        }
    }
}