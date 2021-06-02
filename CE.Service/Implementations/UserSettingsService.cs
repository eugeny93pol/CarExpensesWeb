using System.Threading.Tasks;
using CE.DataAccess;
using CE.DataAccess.DTO;
using CE.Repository;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public class UserSettingsService : BaseService<UserSettings>, IUserSettingsService
    {
        public UserSettingsService(IRepository<UserSettings> repository) : base(repository)
        {
        }

        public async Task UpdatePartial(UserSettings userSettings, UserSettingsDTO settings)
        {
            userSettings.Language = settings.Language ?? userSettings.Language;
            userSettings.Theme = settings.Theme ?? userSettings.Theme;
            userSettings.MeasurementSystem = settings.MeasurementSystem ?? userSettings.MeasurementSystem;
            userSettings.DefaultCarId = settings.DefaultCarId != 0 ? settings.DefaultCarId : userSettings.DefaultCarId;

            await Repository.Update(userSettings);
        }
    }
}
