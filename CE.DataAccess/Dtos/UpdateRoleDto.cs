using System;
using System.ComponentModel.DataAnnotations;

namespace CE.DataAccess.Dtos
{
    public class UpdateRoleDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}