using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections;
using System.ComponentModel;

namespace Lapi4ka.Data.Entitys
{
    public class AnimalType
    {
        public AnimalType()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        [DisplayName("Име")]
        public string Name { get; set; }
        [ValidateNever]
        public ICollection<Animal> Animals { get; set; }
    }
}
