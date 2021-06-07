using System.ComponentModel.DataAnnotations;
using CE.DataAccess.Attributes;

namespace CE.DataAccess
{
    public class CarAction : BaseEntity
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public long Date { get; set; }

        public string Description { get; set; }

        public long CarId { get; set; }
        
    }
}
