using System.ComponentModel.DataAnnotations;
using CE.DataAccess;

namespace CE.WebAPI.RequestModels
{
    public class RegisterRequest : LoginRequest
    {
        [Required]
        public string Name { get; set; }

        public override User GetUser()
        {
            return new() { Name = Name, Email = Email, Password = Password };
        }
    }
}
