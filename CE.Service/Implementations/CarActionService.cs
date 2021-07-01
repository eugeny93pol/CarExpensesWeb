using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess.Models;
using CE.Repository;
using CE.Repository.Interfaces;
using CE.Repository.Repositories;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Implementations
{
    public class CarActionService : ICarActionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly CarActionRepository<CarAction> _carActionRepository;
        private readonly ICarActionTypeService _carActionTypeService;
        private readonly ICarService _carService;

        public CarActionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _carActionRepository = _unitOfWork?.CarActionRepository;
            _carActionTypeService = new CarActionTypeService(_unitOfWork);
            _carService = new CarService(_unitOfWork);
        }

        #region CRUD

        #region CREATE
        public async Task<ActionResult<CarAction>> Create(ClaimsPrincipal claims, CarAction item)
        {
            if (!await _carService.IsUserHasAccessToCar(claims, item.CarId))
                return new ForbidResult();

            if (!await _carActionTypeService.IsActionTypeExist(item.Type))
                return new BadRequestObjectResult($"Type '{item.Type}' does not exist.");

            if (!await CheckDateAndMileage(item))
                return new BadRequestObjectResult($"Invalid mileage {item.Mileage} for date {item.Date}.");

            await _carActionRepository.Create(item);

            return new OkObjectResult(item);
        }

        public async Task<ActionResult<CarActionRepair>> Create(ClaimsPrincipal claims, CarActionRepair item)
        {
            if (!await _carService.IsUserHasAccessToCar(claims, item.CarId))
                return new ForbidResult();

            if (!await _carActionTypeService.IsActionTypeExist(item.Type))
                return new BadRequestObjectResult($"Type '{item.Type}' does not exist.");

            if (!await CheckDateAndMileage(item))
                return new BadRequestObjectResult($"Invalid mileage {item.Mileage} for date {item.Date}.");

            await _carActionRepository.Create(item);

            return new OkObjectResult(item);
        }
        #endregion CREATE

        #region GET
        public async Task<ActionResult<CarAction>> GetOne(
            ClaimsPrincipal claims, 
            Guid id, 
            params Expression<Func<CarAction, object>>[] includeProperties)
        {
            var carAction = await _carActionRepository.GetById(id, includeProperties);
            if (carAction == null)
                return new NotFoundObjectResult(new CarAction { Id = id });

            if (!await _carService.IsUserHasAccessToCar(claims, carAction.CarId))
                return new ForbidResult();

            return new OkObjectResult(carAction);
        }

        public async Task<ActionResult<IEnumerable<CarAction>>> GetAll(
            ClaimsPrincipal claims = null, 
            Expression<Func<CarAction, bool>> filter = null, 
            Func<IQueryable<CarAction>, IOrderedQueryable<CarAction>> orderBy = null,
            params Expression<Func<CarAction, object>>[] includeProperties)
        {
            var carActions = await _carActionRepository.GetAll(filter, orderBy, includeProperties);

            if (UserService.IsHasAccess(claims)) 
                return new OkObjectResult(carActions.ToList());

            var carIdArray = await _carService.GetCarsIdsOfCurrentUser(claims);
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
            var car = await _unitOfWork.CarRepository.GetById(carId);

            if (car == null)
                return new NotFoundObjectResult(new CarAction { CarId = carId });

            if (!await _carService.IsUserHasAccessToCar(claims, carId, car))
                return new ForbidResult();

            var carActions = (await _carActionRepository.GetAll(filter, orderBy, includeProperties))
                .Where(action => action.CarId == carId);

            return ActionsWithSummary(carActions);
        }
        #endregion GET

        #region UPDATE
        public async Task<ActionResult<CarAction>> Update(ClaimsPrincipal claims, CarAction item)
        {
            var carAction = await _carActionRepository.FirstOrDefault(a => a.Id == item.Id);

            if (carAction == null)
                return new NotFoundObjectResult(new CarAction { Id = item.Id });

            if (!await _carService.IsUserHasAccessToCar(claims, carAction.CarId))
                return new ForbidResult();

            throw new NotImplementedException();
        }

        #endregion UPDATE

        #region DELETE
        public async Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id)
        {
            var carAction = await _carActionRepository.GetById(id);

            if (carAction == null)
                return new NotFoundObjectResult(new CarAction { Id = id });

            if (!await _carService.IsUserHasAccessToCar(claims, carAction.CarId))
                return new ForbidResult();

            await _carActionRepository.Remove(carAction);

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

            await _carActionRepository.Update(savedAction);
        }

        #region PRIVATE
        private async Task<bool> CheckDateAndMileage(CarAction action)
        {
            var beforeActions = (await _carActionRepository.GetAll(a => a.CarId == action.CarId))
                .Where(a => a.Date < action.Date && a.Id != action.Id)
                .OrderBy(a => a.Date).ToList();

            var afterActions = (await _carActionRepository.GetAll(a => a.CarId == action.CarId))
                .Where(a => a.Date >= action.Date && a.Id != action.Id)
                .OrderBy(a => a.Date).ToList();

            if (beforeActions.Any() && beforeActions.Last().Mileage > action.Mileage ||
                afterActions.Any() && afterActions.First().Mileage < action.Mileage)
                return false;

            return true;
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
