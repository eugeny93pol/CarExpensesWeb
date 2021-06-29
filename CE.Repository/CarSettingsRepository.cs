using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository
{
    public class CarSettingsRepository : Repository<CarSettings>, ICarSettingsRepository
    {
        public CarSettingsRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
