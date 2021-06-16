using System.Threading.Tasks;
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

        public async Task<bool> IsActionTypeExist(string name)
        {
            var actionType = await Repository.FirstOrDefault(a => a.Name == name);
            return actionType != null;
        }
    }
}
