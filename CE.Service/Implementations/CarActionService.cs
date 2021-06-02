using CE.DataAccess;
using CE.Repository;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public class CarActionService : BaseService<CarAction>, ICarActionService
    {
        public CarActionService(IRepository<CarAction> repository) : base(repository)
        {
        }
    }
}
