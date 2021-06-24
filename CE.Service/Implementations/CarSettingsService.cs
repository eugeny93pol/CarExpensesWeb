using CE.DataAccess;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public class CarSettingsService : BaseService<CarSettings>, ICarSettingsService
    {
        public CarSettingsService(IGenericRepository<CarSettings> genericRepository) : base(genericRepository)
        {
        }
    }
}
