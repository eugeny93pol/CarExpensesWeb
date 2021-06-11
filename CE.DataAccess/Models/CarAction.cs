using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE.DataAccess
{
    public class CarAction : BaseEntity
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public int? Mileage { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public string Description { get; set; }

        public Guid CarId { get; set; }
        
    }
}
