using System;
using System.ComponentModel.DataAnnotations;
using CE.DataAccess.Constants;

namespace CE.DataAccess.Models
{
    public class CarAction : BaseEntity
    {
        [Required]
        public string Type { get; protected set; }

        [Required]
        public uint? Mileage { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        public string Description { get; set; }

        [Required]
        public Guid CarId { get; set; }

        public CarAction()
        {
            Type = CarActionTypesConstants.Default;
        }
        
    }
}
