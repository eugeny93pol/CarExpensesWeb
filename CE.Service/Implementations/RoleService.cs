using CE.DataAccess;
using CE.Repository;

namespace CE.Service
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IRepository<Role> repository) : base(repository)
        {
        }
    }
}
