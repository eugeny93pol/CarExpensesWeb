using System.Threading.Tasks;
using CE.DataAccess;

namespace CE.Service.Interfaces
{
    public interface IActionTypeService : IBaseService<ActionType>
    {
        Task<bool> IsActionTypeExist(string name);
    }
}
