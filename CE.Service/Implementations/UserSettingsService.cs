using CE.DataAccess;
using CE.Repository;
using System.Threading.Tasks;

namespace CE.Service
{
    public class UserSettingsService : BaseService<UserSettings>, IUserSettingsService
    {
        public UserSettingsService(IRepository<UserSettings> repository) : base(repository)
        {
        }

        public async Task UpdatePartial(UserSettings userSettings, UserSettingsDTO settings)
        {
            userSettings.Language = settings.Language == null ? 
                userSettings.Language : 
                settings.Language;
            userSettings.Theme = settings.Theme == null ? 
                userSettings.Theme : 
                settings.Theme;
            userSettings.MeasurementSystem = settings.MeasurementSystem == null ? 
                userSettings.MeasurementSystem : 
                settings.MeasurementSystem;

            await _repository.Update(userSettings);
        }
    }
}
