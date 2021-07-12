using System;
using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess.Dtos
{
    public class UserSettingsDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public Guid? DefaultCarId { set; get; }
        
        public string Language { get; set; }

        public string Theme { get; set; }

        public string MeasurementSystem { set; get; }
    }
}