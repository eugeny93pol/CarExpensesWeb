using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess
{
    public class Car : BaseEntity
    {
        [Required]
        public string Brand { get; set; }

        public string Model { get; set; }

        public ushort Year { get; set; }

        [MaxLength(17)]
        public string VIN { get; set; }

        public CarSettings Settings { get; set; }

        //Navigation
        public long UserId { get; set; }

        public ICollection<CarAction> Actions { get; set; }
    }
}
