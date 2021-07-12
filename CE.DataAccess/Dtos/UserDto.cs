using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CE.DataAccess.Models;

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

        public UserSettings Settings { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}