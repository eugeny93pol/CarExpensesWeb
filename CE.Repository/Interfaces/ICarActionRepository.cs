using System.Threading.Tasks;
using CE.DataAccess.Models;

namespace CE.Repository.Interfaces
{
    public interface ICarActionRepository
    {
        Task<CarActionRepair> Create(CarActionRepair item);
    }
}
