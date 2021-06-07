using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess
{
    public class ActionType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<CarAction> Actions { get; set; }

        public ActionType()
        {
            Actions = new List<CarAction>();
        }
    }
}
