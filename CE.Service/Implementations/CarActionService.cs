using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess.Models;
using CE.Repository;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CE.Service.Implementations
{
    public class CarActionService : ICarActionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ICarActionRepository _carActionRepository;
        private readonly ICarService _carService;
        private readonly ICarActionRefillService _carActionRefillService;
        private readonly ICarActionRepairService _carActionRepairService;

        public CarActionService(IUnitOfWork unitOfWork, ICarService carService)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _carActionRepository = _unitOfWork?.CarActionRepository;
            _carService = carService;
            _carActionRefillService = new CarActionRefillService(_unitOfWork);
            _carActionRepairService = new CarActionRepairService(_unitOfWork);
        }

        #region CRUD

        #region CREATE
        public async Task<ActionResult<T>> Create<T>(ClaimsPrincipal claims, T item) where T : CarAction
        {
            var checkResult = await CheckBeforeCreating(claims, item);
            if (checkResult != null)
                return checkResult;

            switch (item)
            {
                case CarActionRepair repair:
                    await _carActionRepairService.CreateRepair(repair);
                    break;

                case CarActionRefill refill:
                    await _carActionRefillService.CreateRefill(refill);
                    break;

                default:
                    await GetRepositoryByType<T>().Create(item);
                    break;
            }

            return new OkObjectResult(item);
        }
        #endregion CREATE

        #region GET
        public async Task<ActionResult<T>> GetOne<T>(
            ClaimsPrincipal claims, 
            Guid id, 
            params Expression<Func<T, object>>[] includeProperties) where T : CarAction
        {
            var repository = GetRepositoryByType<T>();

            var carAction = await repository.GetById(id, includeProperties);
            
            var checkResult = await CheckAccessToAction(claims, carAction);

            return checkResult ?? new OkObjectResult(carAction);
        }

        public async Task<ActionResult<IEnumerable<T>>> GetAll<T>(
            ClaimsPrincipal claims = null, 
            Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties) where T : CarAction
        {
            var repository = GetRepositoryByType<T>();
            var carActions = await repository.GetAll(filter, orderBy, includeProperties);

            if (UserService.IsHasAccess(claims)) 
                return new OkObjectResult(carActions.ToList());

            var carsId = await _carService.GetCarsIdsOfCurrentUser(claims);
            carActions = carActions.Where(action => carsId.Contains(action.CarId));

            return new OkObjectResult(carActions.ToList());
        }

        public async Task<ActionResult<IEnumerable<T>>> GetActionsByCarId<T>(
            ClaimsPrincipal claims, Guid carId,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties) where T : CarAction
        {
            var car = await _unitOfWork.CarRepository.GetById(carId);

            if (car == null)
                return new NotFoundObjectResult("Car with the provided ID not found.");

            if (!await _carService.IsUserHasAccessToCar(claims, carId, car))
                return new ForbidResult();

            var repository = GetRepositoryByType<T>();

            var carActions = (await repository.GetAll(filter, orderBy, includeProperties))
                .Where(action => action.CarId == carId);

            return new OkObjectResult(carActions.ToList());
        }
        #endregion GET

        #region UPDATE
        public async Task<IActionResult> Update<T>(ClaimsPrincipal claims, T item) where T : CarAction
        {
            var checkResult = await CheckBeforeUpdating(claims, item);
            if (checkResult != null)
                return checkResult;

            switch (item)
            {
                case CarActionRepair repair:
                    return await _carActionRepairService.UpdateRepair(repair);

                case CarActionRefill refill:
                    return await _carActionRefillService.UpdateRefill(refill);

                default:
                    await GetRepositoryByType<T>().Update(item);
                    break;
            }

            return new NoContentResult();
        }
        #endregion UPDATE

        #region DELETE
        public async Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id)
        {
            var carAction = await _carActionRepository.Actions.GetById(id);

            var checkAccessResult = await CheckAccessToAction(claims, carAction);

            if (checkAccessResult != null)
                return checkAccessResult;

            await _carActionRepository.Actions.Remove(carAction);

            return new NoContentResult();
        }
        #endregion DELETE

        #endregion CRUD

        #region PRIVATE
        private async Task<ActionResult> CheckBeforeCreating(ClaimsPrincipal claims, CarAction item)
        {
            if (!await _carService.IsUserHasAccessToCar(claims, item.CarId))
                return new ForbidResult();

            if (!await CheckDateAndMileage(item))
                return new BadRequestObjectResult($"Invalid mileage {item.Mileage} for date {item.Date}.");

            return null;
        }

        private async Task<ActionResult> CheckBeforeUpdating(ClaimsPrincipal claims, CarAction item)
        {
            var checkResult = await CheckBeforeCreating(claims, item);

            if (checkResult != null)
                return checkResult;

            var carAction = await _carActionRepository.Actions.FirstOrDefault(a => a.Id == item.Id);

            checkResult = await CheckAccessToAction(claims, carAction);

            if (checkResult != null)
                return checkResult;

            return carAction.Type != item.Type
                ? new ConflictObjectResult("The resource with the provided ID is not available for updating by this url.")
                : null;
        }

        private async Task<bool> CheckDateAndMileage(CarAction action)
        {
            var previousAction = (await _carActionRepository.Actions.GetAll(
                    a => a.Id != action.Id && a.CarId == action.CarId && a.Date < action.Date,
                    q => q.OrderBy(a => a.Date)))
                .LastOrDefault();
            
            var nextAction = (await _carActionRepository.Actions.GetAll(
                    a => a.Id != action.Id && a.CarId == action.CarId && a.Date >= action.Date,
                    q => q.OrderBy(a => a.Date)))
                .FirstOrDefault();

            return !(previousAction?.Mileage > action.Mileage) && !(nextAction?.Mileage < action.Mileage);
        }

        private async Task<ActionResult> CheckAccessToAction(ClaimsPrincipal claims, CarAction item)
        {
            if (item == null)
                return new NotFoundObjectResult("Action with the provided ID not found.");

            if (!await _carService.IsUserHasAccessToCar(claims, item.CarId))
                return new ForbidResult();

            return null;
        }

        private IGenericRepository<T> GetRepositoryByType<T>() where T : CarAction
        {
            return typeof(T) switch
            {
                var mileageType when mileageType == typeof(CarActionMileage) =>
                    (IGenericRepository<T>)_carActionRepository.Mileages,

                var refillType when refillType == typeof(CarActionRefill) =>
                    (IGenericRepository<T>)_carActionRepository.Refills,

                var repairType when repairType == typeof(CarActionRepair) =>
                    (IGenericRepository<T>)_carActionRepository.Repairs,

                var actionType when actionType == typeof(CarAction) => 
                    (IGenericRepository<T>)_carActionRepository.Actions,

                _ => throw new UnsupportedContentTypeException($"Unsupported type '{typeof(T)}'")
            };
        }
        #endregion PRIVATE
    }
}
