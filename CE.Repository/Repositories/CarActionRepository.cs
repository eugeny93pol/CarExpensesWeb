using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository.Repositories
{
    public class CarActionRepository : GenericRepository<CarAction>, ICarActionRepository
    {
        public CarActionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
