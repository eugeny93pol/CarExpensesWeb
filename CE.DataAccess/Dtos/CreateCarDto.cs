using System;
using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess.Dtos
{
    public class CreateCarDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public uint Mileage { get; set; }

        public string Model { get; set; }

        public ushort? Year { get; set; }

        [MinLength(17), MaxLength(17)]
        public string Vin { get; set; }
    }
}