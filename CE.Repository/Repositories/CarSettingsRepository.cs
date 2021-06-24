using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository.Repositories
{
    public class CarSettingsRepository : GenericRepository<CarSettings>, ICarSettingsRepository
    {
        public CarSettingsRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
