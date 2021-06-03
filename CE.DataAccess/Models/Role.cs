using System.Collections.Generic;

namespace CE.DataAccess
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
