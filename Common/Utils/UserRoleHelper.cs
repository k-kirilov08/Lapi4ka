using Lapi4ka.Data.Constants;
using Lapi4ka.Data.Entitys;
using Microsoft.AspNetCore.Identity;

namespace Lapi4ka.Common.Utils
{
    public static class UserRoleHelper
    {
            public static async Task<bool> IsAdmin(
                UserManager<ApplicationUser> userManager,
                ApplicationUser user)
            {
                return await userManager.IsInRoleAsync(user, UserRoles.Admin);
            }

            public static async Task<bool> IsEmployee(
                UserManager<ApplicationUser> userManager,
                ApplicationUser user)
            {
                return await userManager.IsInRoleAsync(user, UserRoles.Employee);
            }

            public static async Task<bool> IsUser(
                UserManager<ApplicationUser> userManager,
                ApplicationUser user)
            {
                return await userManager.IsInRoleAsync(user, UserRoles.User);
            }
        }
}
