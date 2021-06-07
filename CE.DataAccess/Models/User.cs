using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CE.DataAccess.Attributes;
using CE.DataAccess.Constants;

namespace CE.DataAccess
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
