using event_booking.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

public class UserNameViewComponent : ViewComponent
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _dbcontext;

    public UserNameViewComponent(UserManager<IdentityUser> userManager, ApplicationDbContext dbcontext)
    {
        _userManager = userManager;
        _dbcontext = dbcontext;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userName = User.Identity.Name;
        var identityUser = await _userManager.FindByNameAsync(userName);
        var eventUser = _dbcontext.EventUsers.FirstOrDefault(u => u.EventUserId == identityUser.Id);

        return View("FullName", (object)eventUser.FullName);
    }
}
