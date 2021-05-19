using CE.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace CE.WebAPI.Models
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public virtual User GetUser()
        {
            return new User { Email = this.Email, Password = this.Password };
        }
    }
}
