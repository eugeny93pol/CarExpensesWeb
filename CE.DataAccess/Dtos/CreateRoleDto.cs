using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess.Dtos
{
    public class CreateRoleDto
    {
        [Required]
        public string Name { get; set; }
    }
}