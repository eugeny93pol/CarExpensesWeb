using System.Threading.Tasks;
using CE.DataAccess;
using CE.DataAccess.DTO;

namespace CE.Service.Interfaces
{
    public interface IUserSettingsService : IBaseService<UserSettings>
    {
        Task UpdatePartial(UserSettings userSettings, UserSettingsDTO settings);
    }
}
