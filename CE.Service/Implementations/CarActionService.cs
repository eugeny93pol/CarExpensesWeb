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
        protected readonly UnitOfWork UnitOfWork;
        protected readonly ICarActionRepository CarActionRepository;
        protected readonly ICarService CarService;

        public IService<CarActionRepair> Repair { get; }

        public CarActionService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork as UnitOfWork;
            CarActionRepository = UnitOfWork?.CarActionRepository;
            CarService = new CarService(UnitOfWork);
            Repair = new RepairService(this);
        }

        #region REPAIR_SERVICE
        public class RepairService : IService<CarActionRepair>
        {
            private readonly CarActionService _actionService;

            public RepairService(CarActionService actionService)
            {
                _actionService = actionService;
            }

            public async Task<ActionResult<CarActionRepair>> Create(ClaimsPrincipal claims, CarActionRepair item)
            {
                var checkResult = await _actionService.CheckBeforeCreating(claims, item);

                if (checkResult != null)
                    return checkResult;

                CalculateTotalSparePartsCost(item);

                await _actionService.CarActionRepository.Repairs.Create(item);

                return new OkObjectResult(item);
            }

            public async Task<ActionResult<CarActionRepair>> GetOne(
                ClaimsPrincipal claims, Guid id,
                params Expression<Func<CarActionRepair, object>>[] includeProperties)
            {
                var carAction = await _actionService.CarActionRepository.Repairs.GetById(id, includeProperties);

                var checkResult = await _actionService.CheckAccessToAction(claims, carAction);

                return checkResult ?? new OkObjectResult(carAction);
            }

            public async Task<ActionResult<IEnumerable<CarActionRepair>>> GetAll(
                ClaimsPrincipal claims = null, 
                Expression<Func<CarActionRepair, bool>> filter = null, 
                Func<IQueryable<CarActionRepair>, IOrderedQueryable<CarActionRepair>> orderBy = null,
                params Expression<Func<CarActionRepair, object>>[] includeProperties)
            {
                var repairs = await _actionService
                    .CarActionRepository
                    .Repairs
                    .GetAll(filter, orderBy, includeProperties);

                if (UserService.IsHasAccess(claims))
                    return new OkObjectResult(repairs.ToList());

                var guids = await _actionService.CarService.GetCarsIdsOfCurrentUser(claims);
                repairs = repairs.Where(action => guids.Contains(action.CarId));

                return ActionsWithSummary(repairs);
            }

            public async Task<ActionResult<CarActionRepair>> Update(ClaimsPrincipal claims, CarActionRepair item)
            {
                throw new NotImplementedException();
            }

            public async Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id)
            {
                return await _actionService.Delete(claims, id);
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
        #endregion REPAIR_SERVICE

        #region CRUD

        #region CREATE
        public async Task<ActionResult<T>> Create<T>(ClaimsPrincipal claims, T item) where T : CarAction
        {
            var checkResult = await CheckBeforeCreating(claims, item);

            if (checkResult != null)
                return checkResult;

            switch (item)
            {
                case CarActionMileage mileage:
                    await CarActionRepository.Mileages.Create(mileage);
                    break;

                case CarActionRefill refill:
                    await CarActionRepository.Refills.Create(refill);
                    break;

                case CarActionRepair repair:
                    CalculateTotalSparePartsCost(repair);
                    await CarActionRepository.Repairs.Create(repair);
                    break;

                default:
                    throw new UnsupportedContentTypeException($"Unsupported type '{typeof(T)}'");
            }

            return new OkObjectResult(item);
        }

        //
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
        //

        public async Task<ActionResult<CarActionRefill>> Create(ClaimsPrincipal claims, CarActionRefill item)
        {
            var checkResult = await CheckBeforeCreating(claims, item);

            if (checkResult != null) 
                return checkResult;

            await CarActionRepository.Refills.Create(item);
            return new OkObjectResult(item);
        }

        #endregion CREATE

        #region GET
        public async Task<ActionResult<CarAction>> GetOne(
            ClaimsPrincipal claims, 
            Guid id, 
            params Expression<Func<CarAction, object>>[] includeProperties)
        {
            var carAction = await CarActionRepository.Actions.GetById(id, includeProperties);
            if (carAction == null)
                return new NotFoundObjectResult(new CarAction { Id = id });

            if (!await CarService.IsUserHasAccessToCar(claims, carAction.CarId))
                return new ForbidResult();

            return new OkObjectResult(carAction);
        }

        public async Task<ActionResult<IEnumerable<CarAction>>> GetAll(
            ClaimsPrincipal claims = null, 
            Expression<Func<CarAction, bool>> filter = null, 
            Func<IQueryable<CarAction>, IOrderedQueryable<CarAction>> orderBy = null,
            params Expression<Func<CarAction, object>>[] includeProperties)
        {
            var carActions = await CarActionRepository.Actions.GetAll(filter, orderBy, includeProperties);

            if (UserService.IsHasAccess(claims)) 
                return new OkObjectResult(carActions.ToList());

            var carIdArray = await CarService.GetCarsIdsOfCurrentUser(claims);
            carActions = carActions.Where(action => carIdArray.Contains(action.CarId));

            return ActionsWithSummary(carActions);
        }

        public async Task<ActionResult<IEnumerable<CarAction>>> GetActionsByCarId(
            ClaimsPrincipal claims,
            Guid carId,
            Expression<Func<CarAction, bool>> filter = null,
            Func<IQueryable<CarAction>, IOrderedQueryable<CarAction>> orderBy = null,
            params Expression<Func<CarAction, object>>[] includeProperties)
        {
            var car = await UnitOfWork.CarRepository.GetById(carId);

            if (car == null)
                return new NotFoundObjectResult(new CarAction { CarId = carId });

            if (!await CarService.IsUserHasAccessToCar(claims, carId, car))
                return new ForbidResult();

            var carActions = (await CarActionRepository.Actions.GetAll(filter, orderBy, includeProperties))
                .Where(action => action.CarId == carId);

            return ActionsWithSummary(carActions);
        }
        #endregion GET

        #region UPDATE
        public async Task<ActionResult<CarAction>> Update(ClaimsPrincipal claims, CarAction item)
        {
            var carAction = await CarActionRepository.Actions.FirstOrDefault(a => a.Id == item.Id);

            if (carAction == null)
                return new NotFoundObjectResult(new CarAction { Id = item.Id });

            if (!await CarService.IsUserHasAccessToCar(claims, carAction.CarId))
                return new ForbidResult();

            throw new NotImplementedException();
        }

        #endregion UPDATE

        #region DELETE
        public async Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id)
        {
            var carAction = await CarActionRepository.Actions.GetById(id);

            var checkAccessResult = await CheckAccessToAction(claims, carAction);

            if (checkAccessResult != null)
                return checkAccessResult;

            await CarActionRepository.Actions.Remove(carAction);

            return new NoContentResult();
        }
        #endregion DELETE

        #endregion CRUD

        public async Task UpdatePartial(CarAction savedAction, CarAction action)
        {
            //savedAction.Type = action.Type ?? savedAction.Type;
            savedAction.Mileage = action.Mileage ?? savedAction.Mileage;
            savedAction.Date = action.Date ?? savedAction.Date;
            savedAction.Description = action.Description ?? savedAction.Description;
            //savedAction.Amount = action.Amount ?? savedAction.Amount;

            await CheckDateAndMileage(savedAction);

            await CarActionRepository.Actions.Update(savedAction);
        }

        #region PRIVATE
        private async Task<ActionResult> CheckBeforeCreating(ClaimsPrincipal claims, CarAction item)
        {
            if (!await CarService.IsUserHasAccessToCar(claims, item.CarId))
                return new ForbidResult();

            if (!await CheckDateAndMileage(item))
                return new BadRequestObjectResult($"Invalid mileage {item.Mileage} for date {item.Date}.");

            return null;
        }

        private async Task<bool> CheckDateAndMileage(CarAction action)
        {
            var beforeActions = (await CarActionRepository.Actions.GetAll(a => a.CarId == action.CarId))
                .Where(a => a.Date < action.Date && a.Id != action.Id)
                .OrderBy(a => a.Date).ToList();

            var afterActions = (await CarActionRepository.Actions.GetAll(a => a.CarId == action.CarId))
                .Where(a => a.Date >= action.Date && a.Id != action.Id)
                .OrderBy(a => a.Date).ToList();

            return !(beforeActions.Any() && beforeActions.Last()?.Mileage > action.Mileage) && 
                   !(afterActions.Any() && afterActions.First()?.Mileage < action.Mileage);
        }

        private async Task<ActionResult> CheckAccessToAction(ClaimsPrincipal claims, CarAction item)
        {
            if (item == null)
                return new NotFoundObjectResult("Action with the following ID not found.");

            if (!await CarService.IsUserHasAccessToCar(claims, item.CarId))
                return new ForbidResult();

            return null;
        }

        #endregion PRIVATE

        #region PRIVATE_STATIC
        private static OkObjectResult ActionsWithSummary(IEnumerable<CarAction> carActions)
        {
            var actionsList = carActions.ToList();

            var firstActionDate = actionsList.Min(a => a.Date);
            var lastActionDate = actionsList.Max(a => a.Date);
            //var totalAmount = actionsList.Sum(a => a.Amount);
            //var averageAmount = actionsList.Average(a => a.Amount);

            var summary = new
            {
                firstActionDate,
                lastActionDate,
                //totalAmount,
                //averageAmount,
                actionsList.Count
            };

            return new OkObjectResult(new { actions = actionsList, summary });
        }
        #endregion
    }
}
