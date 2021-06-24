using System.Threading.Tasks;
using CE.DataAccess;

namespace CE.Service.Interfaces
{
    public interface IActionTypeService : IService<ActionType>
    {
        Task<bool> IsActionTypeExist(string name);
    }
}
