using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
