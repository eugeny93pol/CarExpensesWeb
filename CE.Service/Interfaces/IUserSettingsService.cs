using System.Threading.Tasks;
using CE.DataAccess.Models;

namespace CE.Service.Interfaces
{
    public interface IUserSettingsService : IBaseService<UserSettings>
    {
        Task UpdatePartial(UserSettings userSettings, UserSettings settings);
    }
}
