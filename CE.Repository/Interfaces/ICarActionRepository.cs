using CE.DataAccess.Models;

namespace CE.Repository.Interfaces
{
    public interface ICarActionRepository
    {
        IGenericRepository<CarAction> Actions { get; }
        IGenericRepository<CarActionMileage> Mileages { get; }
        IGenericRepository<CarActionRefill> Refills { get; }
        IGenericRepository<CarActionRepair> Repairs { get; }
    }
}
