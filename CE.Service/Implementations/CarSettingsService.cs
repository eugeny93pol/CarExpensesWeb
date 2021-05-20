using CE.DataAccess;
using CE.Repository;

namespace CE.Service
{
    public class CarSettingsService : BaseService<CarSettings>, ICarSettingsService
    {
        public CarSettingsService(IRepository<CarSettings> repository) : base(repository)
        {
        }
    }
}
