using System.Collections.Generic;
using System.Threading.Tasks;
using CE.DataAccess;

namespace CE.Service.Interfaces
{
    public interface ICarService : IBaseService<Car>
    {
        Task<IEnumerable<Car>> GetCarsByUserId(long id);

        Task<long[]> GetCarsIdsByUserId(long id);
        Task<bool> IsUserOwnerCar(long userId, long carId);
    }
}
