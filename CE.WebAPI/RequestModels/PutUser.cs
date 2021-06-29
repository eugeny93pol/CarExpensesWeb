using System;
using System.ComponentModel.DataAnnotations;
using CE.DataAccess;

namespace CE.WebAPI.RequestModels
{
    public class PutUser
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

        public User ConvertToUser()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Role = Role,
                Password = Password,
            };
        }
    }
}
