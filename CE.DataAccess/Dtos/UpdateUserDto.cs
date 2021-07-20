using System;
using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Password { get; set; }

        [MinLength(8)]
        public string NewPassword { get; set; }
    }
}