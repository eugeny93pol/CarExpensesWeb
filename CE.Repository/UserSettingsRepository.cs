using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository
{
    public class UserSettingsRepository : Repository<UserSettings>, IUserSettingsRepository
    {
        public UserSettingsRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
