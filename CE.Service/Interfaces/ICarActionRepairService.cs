using System.Threading.Tasks;
using CE.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Interfaces
{
    internal interface ICarActionRepairService
    {
        Task CreateRepair(CarActionRepair repair);

        Task<IActionResult> UpdateRepair(CarActionRepair repair);
    }
}
