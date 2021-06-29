using System.Threading.Tasks;
using CE.DataAccess.Models;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public class UserSettingsService : BaseService<UserSettings>, IUserSettingsService
    {
        public UserSettingsService(IGenericRepository<UserSettings> genericRepository) : base(genericRepository)
        {
        }

        public async Task UpdatePartial(UserSettings userSettings, UserSettings settings)
        {
            userSettings.Language = settings.Language ?? userSettings.Language;
            userSettings.Theme = settings.Theme ?? userSettings.Theme;
            userSettings.MeasurementSystem = settings.MeasurementSystem ?? userSettings.MeasurementSystem;
            userSettings.DefaultCarId = settings.DefaultCarId ?? userSettings.DefaultCarId;

            await GenericRepository.Update(userSettings);
        }
    }
}
