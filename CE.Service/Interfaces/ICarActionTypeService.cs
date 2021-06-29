using System.Threading.Tasks;
using CE.DataAccess.Models;

namespace CE.Service.Interfaces
{
    public interface ICarActionTypeService : IService<CarActionType>
    {
        Task<bool> IsActionTypeExist(string name);
    }
}
