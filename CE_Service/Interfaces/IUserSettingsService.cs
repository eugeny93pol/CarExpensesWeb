using CE.DataAccess;
using System.Threading.Tasks;

namespace CE.Service
{
    public interface IUserSettingsService : IBaseService<UserSettings>
    {
        Task UpdatePartial(UserSettings userSettings, UserSettingsDTO settings);
    }
}
