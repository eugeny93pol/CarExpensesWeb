using System.ComponentModel.DataAnnotations;
using CE.DataAccess;

namespace CE.WebAPI.RequestModels
{
    public class PutUser
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }

        [Required]
        public string Password { get; set; }

        public void UpdateUser(User user)
        {
            user.Name = Name;
            user.Email = Email;
            user.Role = Role;
        }
    }
}
