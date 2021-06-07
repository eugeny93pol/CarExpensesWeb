using CE.DataAccess;
using CE.Repository;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public class ActionTypeService : BaseService<ActionType>, IActionTypeService
    {
        public ActionTypeService(IRepository<ActionType> repository) : base(repository)
        {
        }
    }
}
