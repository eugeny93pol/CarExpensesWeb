using CE.DataAccess;
using CE.Repository;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IRepository<Role> repository) : base(repository)
        {
        }
    }
}
