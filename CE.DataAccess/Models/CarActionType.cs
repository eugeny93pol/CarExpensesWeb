using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess.Models
{
    public class CarActionType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<CarAction> Actions { get; set; }

        public CarActionType()
        {
            Actions = new List<CarAction>();
        }
    }
}
