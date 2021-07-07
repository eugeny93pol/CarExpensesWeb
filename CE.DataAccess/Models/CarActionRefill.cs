using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CE.DataAccess.Constants;

namespace CE.DataAccess.Models
{
    public class CarActionRefill : CarAction
    {
        [Required]
        public ushort Quantity { get; set; }

        [Required]
        public string FuelType { get; set; }

        [DefaultValue(false)]
        public bool IsCheckPoint { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        public CarActionRefill()
        {
            Type = CarActionTypesConstants.Refill;
        }
    }
}
