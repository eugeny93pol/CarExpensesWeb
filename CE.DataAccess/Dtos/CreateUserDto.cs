﻿using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}