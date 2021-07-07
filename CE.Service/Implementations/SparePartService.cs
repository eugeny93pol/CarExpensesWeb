using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.DataAccess.Models;
using CE.Repository;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public class SparePartService : ISparePartService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IGenericRepository<CarActionRepair> _repairRepository;
        private readonly IGenericRepository<SparePart> _sparePartRepository;


        public SparePartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _repairRepository = _unitOfWork?.CarActionRepository.Repairs;
            _sparePartRepository = _unitOfWork?.SparePartRepository;
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

        public async Task UpdateSpareParts(CarActionRepair repair)
        {
            var sparePartRepository = _unitOfWork.SparePartRepository;

            var savedSpareParts = await GetSparePartsByActionId(repair.Id);

            if (repair.SpareParts.Any(sp => sp.Id != Guid.Empty && !GetSparePartsIdArray(savedSpareParts).Contains(sp.Id)))
                throw new ArgumentException("One or more spare parts have an incorrect ID, or belong to another action.");

            if (repair.SpareParts.Any(sp => sp.CarActionRepairId != Guid.Empty && sp.CarActionRepairId != repair.Id))
                throw new ArgumentException("One or more spare parts have an incorrect action ID");

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
        }
    }
}