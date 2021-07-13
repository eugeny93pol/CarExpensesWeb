using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess.Dtos
{
    public class UserDto
    {
        [Required]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        public GetUserSettingsDto Settings { get; set; }

        public ICollection<CarDto> Cars { get; set; }
    }
}