using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CE.DataAccess.Models
{

    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public UserSettings Settings { get; set; }

        public ICollection<Car> Cars { get; set; }

        public User()
        {
            Cars = new List<Car>();
        }

    }
}
