using CE.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace CE.WebAPI.Models
{
    public class RegisterRequest : LoginRequest
    {
        [Required]
        public string Name { get; set; }

        public override User GetUser()
        {
            return new User { Name = this.Name, Email = this.Email, Password = this.Password };
        }
    }
}
