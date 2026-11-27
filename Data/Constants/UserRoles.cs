using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Lapi4ka.Data.Constants
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Employee = "Employee";
        public const string User = "User";

        public static List<SelectListItem> GetRolesList()
        {
            return new List<SelectListItem> {
                new SelectListItem {
                    Value = Employee,
                    Text = "Служител"
                },
                new SelectListItem {
                    Value = User,
                    Text = "Потребител"
                },
                };
        }
    }
}
