using Lapi4ka.Areas.Admin.Models;
using Lapi4ka.Data;
using Lapi4ka.Data.Constants;
using Lapi4ka.Data.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lapi4ka.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        readonly IUserStore<ApplicationUser> _userStore;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
 
        public UsersController(ApplicationDbContext context, IUserStore<ApplicationUser> userStore, UserManager<ApplicationUser> userManager)
        {
            _userStore = userStore;
            _context = context;
            _userManager = userManager;
            _emailStore = GetEmailStore();

        }
        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public IActionResult Create()
        {
            UserViewModel model = new UserViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Password,RepeatPassword,UserRole")] UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser();
                appUser.Email = user.Email;
                appUser.FirstName = user.FirstName;
                appUser.LastName = user.LastName;
                await _userStore.SetUserNameAsync(appUser, user.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(appUser, user.Email, CancellationToken.None);
                
                var result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(appUser, user.UserRole).GetAwaiter().GetResult();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Password",error.Description);
                    }

                }
            }
            
                return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            var roles = await _userManager.GetRolesAsync(appUser);
            var user = new UserViewModel();
            user.Email = appUser.Email;
            user.FirstName = appUser.FirstName;
            user.LastName = appUser.LastName;
            user.UserRole = roles.FirstOrDefault() ?? "";
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,FirstName,LastName,Email,Password,RepeatPassword,UserRole")] UserViewModel user)
        {
            var appUser = await _userManager.FindByIdAsync(user.Id);
            if (appUser==null)
            {
                return NotFound();
            }
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser != null && existingUser.Id != appUser.Id)
            {
                ModelState.AddModelError(
                    nameof(user.Email),
                    "Този имейл вече се използва.");
                return View(user);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    appUser.FirstName = user.FirstName;
                    appUser.LastName = user.LastName;
                    if (appUser.Email != user.Email)
                    {
                        var emailResult = await _userManager.SetEmailAsync(
                            appUser, user.Email);

                        if (!emailResult.Succeeded)
                        {
                            foreach (var error in emailResult.Errors)
                                ModelState.AddModelError(nameof(user.Email), error.Description);

                            return View(user);
                        }

                        var usernameResult = await _userManager.SetUserNameAsync(
                            appUser, user.Email);

                        if (!usernameResult.Succeeded)
                        {
                            foreach (var error in usernameResult.Errors)
                                ModelState.AddModelError(nameof(user.Email), error.Description);

                            return View(user);
                        }
                    }
                    var updateResult = await _userManager.UpdateAsync(appUser);

                    if (!updateResult.Succeeded)
                    {
                        foreach (var error in updateResult.Errors)
                            ModelState.AddModelError("", error.Description);

                        return View(user);
                    }
                    if (!string.IsNullOrWhiteSpace(user.Password))
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

                        var passwordResult = await _userManager.ResetPasswordAsync(
                            appUser, token, user.Password);

                        if (!passwordResult.Succeeded)
                        {
                            foreach (var error in passwordResult.Errors)
                                ModelState.AddModelError(nameof(user.Password), error.Description);

                            return View(user);
                        }
                    }

                    // Смяна на роля
                    var currentRoles = await _userManager.GetRolesAsync(appUser);

                    if (!currentRoles.Contains(user.UserRole))
                    {
                        var removeResult = await _userManager.RemoveFromRolesAsync(
                            appUser, currentRoles);

                        if (!removeResult.Succeeded)
                        {
                            foreach (var error in removeResult.Errors)
                                ModelState.AddModelError(nameof(user.UserRole), error.Description);

                            return View(user);
                        }

                        var addResult = await _userManager.AddToRoleAsync(
                            appUser, user.UserRole);

                        if (!addResult.Succeeded)
                        {
                            foreach (var error in addResult.Errors)
                                ModelState.AddModelError(nameof(user.UserRole), error.Description);

                            return View(user);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
