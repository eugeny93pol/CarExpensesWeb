using System;
using System.Collections.Generic;

namespace CE.DataAccess.Dtos
{
    public class GetRoleDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserDto> Users { get; set; }
    }
}