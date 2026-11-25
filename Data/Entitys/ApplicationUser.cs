using Lapi4ka.Common.MetaData;
using Lapi4ka.Data.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lapi4ka.Data.Entitys
{
    [ModelMetadataType(typeof(ApplicationUserMetadata))]
    public class ApplicationUser:IdentityUser
    {
        [DisplayName("Име")]
        public string FirstName { get; set; }
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [NotMapped]
        [DisplayName("Парола")]
        public string Password { get; set; }
        [NotMapped]
        [DisplayName("Потвърди Парола")]
        public string RepeatPassword { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<Adoption> AdoptionRequests { get; set; }
        public ICollection<Adoption> ProcessedRequests { get; set; }
        public ICollection<Application> Applications { get; set; }
        
    }
}
