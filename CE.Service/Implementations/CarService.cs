using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.DataAccess;
using CE.Repository;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public class CarService : BaseService<Car>, ICarService
    {
        public CarService(IRepository<Car> repository) : base(repository)
        {
        }

        public async Task<IEnumerable<Car>> GetCarsByUserId(long userId)
        {
            return await Repository.GetAll(c => c.UserId == userId);
        }

        public async Task<long[]> GetCarsIdsByUserId(long userId)
        {
            var cars = await GetCarsByUserId(userId);
            return cars.Select(c => c.Id).ToArray();
        }

        public async Task<bool> IsUserOwnerCar(long userId, long carId)
        {
            return (await Repository.GetById(carId))?.UserId == userId;
        }

        public async Task UpdatePartial(Car savedCar, Car car)
        {
            savedCar.Brand = car.Brand ?? savedCar.Brand;
            savedCar.Model = car.Model ?? savedCar.Model;
            savedCar.Vin = car.Vin ?? savedCar.Vin;
            savedCar.Year = car.Year != 0 ? car.Year : savedCar.Year;
            await Repository.Update(savedCar);
        }
    }
}
