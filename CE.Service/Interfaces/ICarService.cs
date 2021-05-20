using CE.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CE.Service
{
    public interface ICarService : IBaseService<Car>
    {
        Task<IEnumerable<Car>> GetCarsByUserId(long id);

        Task<long[]> GetCarsIdsByUserId(long id);
        Task<bool> IsUserOwnerCar(long userId, long carId);
    }
}
