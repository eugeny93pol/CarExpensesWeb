using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
