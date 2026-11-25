using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections;

namespace Lapi4ka.Data.Entitys
{
    public class AnimalType
    {
        public AnimalType()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        [ValidateNever]
        public ICollection<Animal> Animals { get; set; }
    }
}
