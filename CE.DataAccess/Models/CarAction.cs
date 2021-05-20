using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess
{
    public class CarAction : BaseEntity
    {
        [Required]
        public string Type { get; set; }

        public int Mileage { get; set; }

        public long Date { get; set; }

        public string Description { get; set; }

        //Navigation
        public long CarId { get; set; }
        //public Car Car { get; set; }
    }
}
