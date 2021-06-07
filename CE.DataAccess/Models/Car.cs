using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess
{
    public class Car : BaseEntity
    {
        [Required]
        public string Brand { get; set; }

        public string Model { get; set; }

        public ushort? Year { get; set; }

        [MinLength(17), MaxLength(17)]
        public string Vin { get; set; }

        public CarSettings Settings { get; set; }

        public long UserId { get; set; }

        public ICollection<CarAction> Actions { get; set; }

        public Car()
        {
            Actions = new List<CarAction>();
        }
    }
}
