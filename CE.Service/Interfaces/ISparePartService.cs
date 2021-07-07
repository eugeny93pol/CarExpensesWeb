using System.Threading.Tasks;
using CE.DataAccess.Models;

namespace CE.Service.Interfaces
{
    public interface ISparePartService
    {
        Task UpdateSpareParts(CarActionRepair repair);
    }
}
