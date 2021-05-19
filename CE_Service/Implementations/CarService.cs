using CE.DataAccess;
using CE.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CE.Service
{
    public class CarService : BaseService<Car>, ICarService
    {
        public CarService(IRepository<Car> repository) : base(repository)
        {
        }

        public async Task<IEnumerable<Car>> GetCarsByUserId(long userId)
        {
            return await _repository.GetAll(c => c.UserId == userId);
        }

        public async Task<long[]> GetCarsIdsByUserId(long userId)
        {
            var cars = await GetCarsByUserId(userId);
            return cars.Select(c => c.Id).ToArray();
        }

        public async Task<bool> IsUserOwnerCar(long userId, long carId)
        {
            return (await _repository.GetById(carId)).UserId == userId;
        }
        
    }
}
