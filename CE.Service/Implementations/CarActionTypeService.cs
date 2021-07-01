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
    public class CarActionTypeService : ICarActionTypeService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IGenericRepository<CarActionType> _carActionTypeRepository;
        private readonly ICarService _carService;

        public CarActionTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _carActionTypeRepository = _unitOfWork?.CarActionTypeRepository;
            _carService = new CarService(_unitOfWork);
        }

        #region CRUD

        #region CREATE
        public async Task<ActionResult<CarActionType>> Create(ClaimsPrincipal claims, CarActionType item)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var actionType = await _carActionTypeRepository.FirstOrDefault(a => a.Name == item.Name);
            if (actionType != null)
                return new BadRequestObjectResult($"The action type named '{item.Name}' already exists.");
                    
            await _carActionTypeRepository.Create(item);
            return new OkObjectResult(item);
        }
        #endregion CREATE

        #region GET
        public async Task<ActionResult<CarActionType>> GetOne(
            ClaimsPrincipal claims, Guid id, 
            params Expression<Func<CarActionType, object>>[] includeProperties)
        {
            var actionType = await _carActionTypeRepository.GetById(id, includeProperties);

            if (actionType == null)
                return new NotFoundObjectResult(new CarActionType {Id = id});

            if (includeProperties.Length == 0 || UserService.IsHasAccess(claims))
                return new OkObjectResult(actionType);

            var carIds = await _carService.GetCarsIdsOfCurrentUser(claims);
            var userActions = actionType.Actions.Where(a => carIds.Contains(a.CarId)).ToList();

            actionType.Actions = userActions;

            return new OkObjectResult(actionType);
        }

        public async Task<ActionResult<IEnumerable<CarActionType>>> GetAll(
            ClaimsPrincipal claims = null,
            Expression<Func<CarActionType, bool>> filter = null,
            Func<IQueryable<CarActionType>, IOrderedQueryable<CarActionType>> orderBy = null,
            params Expression<Func<CarActionType, object>>[] includeProperties)
        {
            var actionTypes = await _carActionTypeRepository.GetAll(filter, orderBy, includeProperties);

            if (includeProperties.Length == 0 || UserService.IsHasAccess(claims))
                return new OkObjectResult(actionTypes.ToList());

            var carIds = await _carService.GetCarsIdsOfCurrentUser(claims);
            var actionTypesList = actionTypes.ToList();

            foreach (var actionType in actionTypesList)
            {
                var userActions = actionType.Actions.Where(a => carIds.Contains(a.CarId)).ToList();
                actionType.Actions = userActions;
            }

            return new OkObjectResult(actionTypesList);
        }
        #endregion GET

        #region UPDATE
        public async Task<ActionResult<CarActionType>> Update(ClaimsPrincipal claims, CarActionType item)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var actionType = await _carActionTypeRepository.FirstOrDefault(a => a.Name == item.Name);
            if (actionType != null)
                return new BadRequestObjectResult($"The action type named '{item.Name}' already exists.");

            actionType = await _carActionTypeRepository.FirstOrDefault(a => a.Id == item.Id);
            if (actionType == null)
                return new NotFoundObjectResult(new CarActionType {Id = item.Id});

            var actions = (await _carActionTypeRepository.GetById(item.Id, a => a.Actions)).Actions;
            var actionTypeToCreate = new CarActionType {Name = item.Name};

            await _carActionTypeRepository.Create(actionTypeToCreate);

            //foreach (var carAction in actions)
            //{
            //    carAction.Type = actionTypeToCreate.Name;
            //    await _unitOfWork.CarActionRepository.Update(carAction);
            //}

            await _carActionTypeRepository.Remove(actionType);

            return new OkObjectResult(actionTypeToCreate);
        }
        #endregion UPDATE

        #region DELETE
        public async Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var actionType = await _carActionTypeRepository.GetById(id, a => a.Actions);
            if (actionType == null)
                return new NotFoundObjectResult(new CarActionType { Id = id });

            if (actionType.Actions.Count > 0)
                return new BadRequestObjectResult(
                    $"The '{actionType.Name}' type with id={{{id}}} cannot be deleted, because there are Actions with this type.");

            await _carActionTypeRepository.Remove(actionType);

            return new NoContentResult();
        }
        #endregion DELETE

        #endregion CRUD


        public async Task<bool> IsActionTypeExist(string name)
        {
            var actionType = await _carActionTypeRepository.FirstOrDefault(a => a.Name == name);
            return actionType != null;
        }
    }
}
