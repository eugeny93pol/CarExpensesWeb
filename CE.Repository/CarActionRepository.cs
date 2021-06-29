using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository
{
    public class CarActionRepository : Repository<CarAction>, ICarActionRepository
    {
        public CarActionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
