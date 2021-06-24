using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository.Repositories
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
