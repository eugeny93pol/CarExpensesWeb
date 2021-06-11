using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CE.DataAccess;

namespace CE.Service.Interfaces
{
    public interface ICarService : IBaseService<Car>
    {
        Task<IEnumerable<Car>> GetCarsByUserId(Guid id);

        Task<Guid[]> GetCarsIdsByUserId(Guid id);

        Task<bool> IsUserOwnerCar(Guid userId, Guid carId);

        Task UpdatePartial(Car savedCar, Car car);
    }
}
