using System;
using System.Threading.Tasks;
using CE.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Interfaces
{
    internal interface ICarActionRefillService
    {
        Task CreateRefill(CarActionRefill refill);

        Task<IActionResult> UpdateRefill(CarActionRefill refill);

        internal Task UpdateAverageFuelConsumptions(Guid carId);
    }
}