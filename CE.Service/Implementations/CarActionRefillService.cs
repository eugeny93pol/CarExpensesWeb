using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CE.DataAccess.Constants;
using CE.DataAccess.Models;
using CE.Repository;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Implementations
{
    //TODO: make this class internal
    internal class CarActionRefillService : ICarActionRefillService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IGenericRepository<CarActionRefill> _refillRepository;

        public CarActionRefillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _refillRepository = _unitOfWork?.CarActionRepository.Refills;
        }

        public async Task CreateRefill(CarActionRefill refill)
        {
            var type = await GetFuelConsumptionType(refill.CarId);

            if (refill.IsCheckPoint)
            {
                var refills = await GetRefills(refill.CarId);
                CalculateAverageFuelConsumption(refills, refill, type);
            }

            await _refillRepository.Create(refill);
            await CheckAndUpdateNextCheckPoint(refill, type);
        }

        public async Task<IActionResult> UpdateRefill(CarActionRefill refill)
        {
            var type = await GetFuelConsumptionType(refill.CarId);
            var refills = await GetRefills(refill.CarId);

            var savedRefill = refills.FirstOrDefault(r => r.Id == refill.Id);
            if (savedRefill == null)
                return new NotFoundObjectResult("Action with the provided ID not found.");

            refills.Remove(savedRefill);
            await CheckAndUpdateNextCheckPoint(savedRefill, type, refills);
            
            if (refill.IsCheckPoint)
            {
                CalculateAverageFuelConsumption(refills, refill, type);
            }

            await _refillRepository.Update(refill);
            await CheckAndUpdateNextCheckPoint(refill, type);

            return new NoContentResult();
        }


        async Task ICarActionRefillService.UpdateAverageFuelConsumptions(Guid carId)
        {
            var type = await GetFuelConsumptionType(carId);

            var refills = await GetRefills(carId);
            var checkPoints = refills.Where(r => r.IsCheckPoint);

            foreach (var checkPoint in checkPoints)
            {
                CalculateAverageFuelConsumption(refills, checkPoint, type);
                await _refillRepository.Update(checkPoint);
            }
        }

        private static void CalculateAverageFuelConsumption(IReadOnlyCollection<CarActionRefill> refills,
            CarActionRefill currentCheckPoint, FuelConsumptionType type)
        {
            var previousCheckPoint = refills.LastOrDefault(r => r.IsCheckPoint && r.Date < currentCheckPoint.Date);
            if (previousCheckPoint != null)
            {
                CalculateAverageBetweenTwoCheckPoints(refills, previousCheckPoint, currentCheckPoint, type);
            }
        }

        private async Task CheckAndUpdateNextCheckPoint(CarActionRefill currentRefill, FuelConsumptionType type,
            List<CarActionRefill> refills = null)
        {
            refills ??= await GetRefills(currentRefill.CarId);

            var nextCheckPoint = refills.FirstOrDefault(r => r.IsCheckPoint && r.Date > currentRefill.Date);
            if (nextCheckPoint != null)
            {
                var previousCheckPoint = refills.LastOrDefault(r => r.IsCheckPoint && r.Date <= currentRefill.Date);

                if (previousCheckPoint != null)
                {
                    CalculateAverageBetweenTwoCheckPoints(refills, previousCheckPoint, nextCheckPoint, type);
                }
                else
                {
                    nextCheckPoint.AverageFuelConsumption = null;
                }

                await _refillRepository.Update(nextCheckPoint);
            }
        }

        private static void CalculateAverageBetweenTwoCheckPoints(IEnumerable<CarActionRefill> refills,
            CarActionRefill previousCheckPoint, CarActionRefill currentCheckPoint, FuelConsumptionType type)
        {
            var refillsBetweenCheckPoints =
                refills.Where(r => r.Date > previousCheckPoint.Date && r.Date < currentCheckPoint.Date);

            var fuelQuantity = previousCheckPoint.Quantity +
                               refillsBetweenCheckPoints.Aggregate(0, (current, refill) => current + refill.Quantity);

            var distance = currentCheckPoint.Mileage - previousCheckPoint.Mileage;

            currentCheckPoint.AverageFuelConsumption = type switch
            {
                FuelConsumptionType.VolumePer100DistanceUnits =>
                    distance == 0 ? null : (decimal)fuelQuantity / distance * 100,

                FuelConsumptionType.DistancePerVolumeUnit =>
                    fuelQuantity == 0 ? null : (decimal)distance / fuelQuantity,

                _ => null
            };
        }

        private async Task<List<CarActionRefill>> GetRefills(Guid carId)
        {
            var refills = await _refillRepository.GetAll(r => r.CarId == carId, o => o.OrderBy(r => r.Date));
            return refills.ToList();
        }

        private async Task<FuelConsumptionType> GetFuelConsumptionType(Guid carId)
        {
            var car = await _unitOfWork.CarRepository.GetById(carId, c => c.Settings);
            return car.Settings.FuelConsumptionType;
        }
    }
}