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

            // Add a hosted service that will run the cleanup task in the background
            builder.Services.AddHostedService<ClearExpiredSessionsService>();

            //Search service initialization
            builder.Services.AddScoped<EventSearchService>();

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

// ClearExpiredSessionsService IHostedService
public class ClearExpiredSessionsService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ClearExpiredSessionsService> _logger;

    public ClearExpiredSessionsService(IServiceProvider serviceProvider, ILogger<ClearExpiredSessionsService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting ClearExpiredSessionsService.");

            // Start the cleanup task
            Task.Run(() => ClearExpiredSessionsTask(), cancellationToken);

            // Immediately return a completed task
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "Error starting ClearExpiredSessionsService.");
            throw;
        }
    }

    //Stops the service
    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Nothing to do here
        return Task.CompletedTask;
    }

    // Runs every 5 minutes
    private async Task ClearExpiredSessionsTask()
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            while (true)
            {
                await ClearExpiredSessions(dbContext);

                // Wait for 5 minutes before running the task again
                await Task.Delay(TimeSpan.FromMinutes(5));
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "Error in ClearExpiredSessionsTask.");
            throw;
        }
    }

    private async Task ClearExpiredSessions(ApplicationDbContext dbContext)
    {
        // Get the current time
        var currentTime = DateTime.UtcNow;

        // Get all purchases whose session has expired
        var expiredPurchases = await dbContext.Purchases
            .Where(p => p.SessionExpiryTime <= currentTime)
            .ToListAsync();

        // Clear the PurchaseId from the tickets associated with each expired session
        foreach (var purchase in expiredPurchases)
        {
            if (purchase == null)
            {
                continue;
            }

            var tickets = await dbContext.Tickets
                .Where(t => t.PurchaseId == purchase.PurchaseId)
                .ToListAsync();

            foreach (var ticket in tickets)
            {
                ticket.PurchaseId = null;
            }

            // Remove the associated Sales before removing the Purchase
            var sales = await dbContext.Sales
                .Where(s => s.PurchaseId == purchase.PurchaseId)
                .ToListAsync();

            foreach (var sale in sales)
            {
                dbContext.Sales.Remove(sale);
            }

            // Remove the associated TicketGroup before removing the Purchase
            var ticketGroups = await dbContext.TicketGroups
                .Where(tg => tg.PurchaseId == purchase.PurchaseId)
                .ToListAsync();

            foreach (var ticketGroup in ticketGroups)
            {
                dbContext.TicketGroups.Remove(ticketGroup);
            }

            // Remove the expired purchase
            dbContext.Purchases.Remove(purchase);
        }

        await dbContext.SaveChangesAsync();
    }
}