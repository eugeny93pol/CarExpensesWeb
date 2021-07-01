using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CE.DataAccess.Constants;

namespace CE.DataAccess.Models
{
    public class CarActionRepair : CarAction
    {
        [Column(TypeName = "money")]
        public decimal CostOfWork { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public ICollection<SparePart> SpareParts { get; set; }

        public CarActionRepair()
        {
            Type = CarActionTypesConstants.Repair;
        }
    }
}
