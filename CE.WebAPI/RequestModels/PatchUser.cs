using System.ComponentModel.DataAnnotations;
using CE.DataAccess;

namespace CE.WebAPI.RequestModels
{
    public class PatchUser
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

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