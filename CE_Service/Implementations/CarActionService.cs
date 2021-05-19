using CE.DataAccess;
using CE.Repository;

namespace CE.Service
{
    public class CarActionService : BaseService<CarAction>, ICarActionService
    {
        public CarActionService(IRepository<CarAction> repository) : base(repository)
        {
        }
    }
}
