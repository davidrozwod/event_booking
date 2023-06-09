using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Data;

[Authorize(Roles = "Admin")]
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    // GET: /User/Index
    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User deleted successfully";
                return RedirectToAction("Index", "Account");
            }
            else
            {
                TempData["ErrorMessage"] = "Could not delete user";
                // Failed to delete user
                // Handle the error accordingly
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Could not find user";
        }

        return RedirectToAction("Index", "Account");
    }
}
