using CE.DataAccess;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public class CarSettingsService : BaseService<CarSettings>, ICarSettingsService
    {
        public CarSettingsService(IRepository<CarSettings> repository) : base(repository)
        {
        }
    }
}
