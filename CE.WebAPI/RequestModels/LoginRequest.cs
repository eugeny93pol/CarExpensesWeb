using System.ComponentModel.DataAnnotations;
using CE.DataAccess.Models;

namespace CE.WebAPI.RequestModels
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }

        public virtual User GetUser()
        {
            return new() { Email = Email, Password = Password };
        }
    }
}
