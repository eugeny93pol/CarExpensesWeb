using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository.Repositories
{
    public class CarActionTypeRepository : GenericRepository<CarActionType>, ICarActionTypeRepository
    {
        public CarActionTypeRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
