using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Lapi4ka.Areas.Admin.Models
{
    public class UserViewModel
    {

        public string? Id { get; set; }
        [DisplayName("Име")]
        public string FirstName { get; set; }
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [DisplayName("Парола")]
        [Required]
        [StringLength(100, ErrorMessage = "Паролата трябва да е минимум 6 знака", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Потвърди Парола")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролите не си съвпадат")]
        public string RepeatPassword { get; set; }
        public List<SelectListItem> UserRoles = Lapi4ka.Data.Constants.UserRoles.GetRolesList();
        public string UserRole { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
