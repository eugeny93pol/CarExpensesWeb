using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.DataAccess.Models;
using CE.Repository;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Implementations
{
    internal class CarActionRepairService : ICarActionRepairService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IGenericRepository<CarActionRepair> _repairRepository;
        private readonly IGenericRepository<SparePart> _sparePartRepository;


        public CarActionRepairService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _repairRepository = _unitOfWork?.CarActionRepository.Repairs;
            _sparePartRepository = _unitOfWork?.SparePartRepository;
        }

        public async Task CreateRepair(CarActionRepair repair)
        {
            CalculateTotalSparePartsCost(repair);
            await _repairRepository.Create(repair);
        }

        public async Task<IActionResult> UpdateRepair(CarActionRepair repair)
        {
            var sparePartRepository = _unitOfWork.SparePartRepository;

            var savedSpareParts = await GetSparePartsByActionId(repair.Id);

            if (repair.SpareParts.Any(sp => sp.Id != Guid.Empty && !GetSparePartsIdArray(savedSpareParts).Contains(sp.Id)))
                return new BadRequestObjectResult("One or more spare parts have an incorrect ID, or belong to another action.");

            if (repair.SpareParts.Any(sp => sp.CarActionRepairId != Guid.Empty && sp.CarActionRepairId != repair.Id))
                return new BadRequestObjectResult("One or more spare parts have an incorrect action ID");

            var sparePartsToRemove = savedSpareParts
                .Where(sp => !GetSparePartsIdArray(repair.SpareParts).Contains(sp.Id));

            await RemoveSpareParts(sparePartsToRemove);

            foreach (var sparePart in repair.SpareParts)
            {
                sparePart.CarActionRepairId = repair.Id;
                if (sparePart.Id != Guid.Empty)
                {
                    await sparePartRepository.Update(sparePart);
                }
                else
                {
                    await sparePartRepository.Create(sparePart);
                }
            }

            CalculateTotalSparePartsCost(repair);
            await _repairRepository.Update(repair);

            return new NoContentResult();
        }
        private async Task<ICollection<SparePart>> GetSparePartsByActionId(Guid id)
        {
            return (await _repairRepository.FirstOrDefault(a
                => a.Id == id, r => r.SpareParts)).SpareParts;
        }

        private async Task RemoveSpareParts(IEnumerable<SparePart> spareParts)
        {
            foreach (var sparePart in spareParts)
            {
                await _sparePartRepository.Remove(sparePart);
            }
        }

        private static IEnumerable<Guid> GetSparePartsIdArray(IEnumerable<SparePart> spareParts)
        {
            return spareParts.Select(sp => sp.Id).ToArray();
        }

        private static void CalculateTotalSparePartsCost(CarActionRepair item)
        {
            if (item.Total.HasValue) return;

            item.Total = 0;
            foreach (var sparePart in item.SpareParts)
            {
                if (sparePart.Price.HasValue)
                    item.Total += sparePart.Price * sparePart.Quantity;
            }
        }
    }
}