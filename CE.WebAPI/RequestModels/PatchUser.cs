using System.ComponentModel.DataAnnotations;
using CE.DataAccess;

namespace CE.WebAPI.RequestModels
{
    public class PatchUser
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public User GetUser()
        {
            return new()
            {
                Name = this.Name,
                Email = this.Email,
                Password = this.Password
            };
        }
    }
}