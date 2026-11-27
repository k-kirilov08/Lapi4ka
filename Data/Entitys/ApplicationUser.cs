using Lapi4ka.Common.MetaData;
using Lapi4ka.Data.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        public DateTime RegistrationDate= DateTime.Now;
        public ICollection<Adoption> AdoptionRequests { get; set; }
        public ICollection<Adoption> ProcessedRequests { get; set; }
        public ICollection<Application> Applications { get; set; }
        
    }
}
