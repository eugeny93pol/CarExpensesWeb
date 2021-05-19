using CE.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace CE.WebAPI.Models
{
    public class PutUserRequest
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }

        public User GetUser(User user)
        {
            return new User
            {
                Id = this.Id,
                Name = this.Name,
                Email = this.Email,
                Password = user.Password,
                Role = user.Role
            };
        }
    }
}
