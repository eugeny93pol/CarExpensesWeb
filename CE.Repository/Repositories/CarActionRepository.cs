using CE.DataAccess.Models;
using CE.Repository.Interfaces;

namespace CE.Repository.Repositories
{
    public class CarActionRepository : ICarActionRepository
    {
        public IGenericRepository<CarAction> Actions { get; }
        public IGenericRepository<CarActionMileage> Mileages { get; }
        public IGenericRepository<CarActionRefill> Refills { get; }
        public IGenericRepository<CarActionRepair> Repairs { get; }


        public CarActionRepository(ApplicationContext context)
        {
            Actions = new GenericRepository<CarAction>(context);
            Mileages = new GenericRepository<CarActionMileage>(context);
            Refills = new GenericRepository<CarActionRefill>(context);
            Repairs = new GenericRepository<CarActionRepair>(context);
        }
    }
}
