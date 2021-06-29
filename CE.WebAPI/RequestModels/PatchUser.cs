using System;
using System.ComponentModel.DataAnnotations;
using CE.DataAccess.Models;

namespace CE.WebAPI.RequestModels
{
    public class PatchUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Role { get; set; }

        public string Password { get; set; }

        public User ConvertToUser()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Role = Role,
                Password = Password
            };
        }
    }
}