using System.Threading.Tasks;
using CE.DataAccess;

namespace CE.Service.Interfaces
{
    public interface ICarActionService : IBaseService<CarAction>
    {
        Task UpdatePartial(CarAction savedAction, CarAction action);
    }
}
