using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository
{
    public class ActionTypeRepository : Repository<ActionType>, IActionTypeRepository
    {
        public ActionTypeRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
