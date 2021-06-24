using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess;
using CE.Repository;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Implementations
{
    public class ActionTypeService : IActionTypeService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ActionTypeRepository _actionTypeRepository;
        private readonly ICarService _carService;

        public ActionTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _actionTypeRepository = _unitOfWork?.ActionTypeRepository;
            _carService = new CarService(_unitOfWork);
        }

        #region CRUD

        #region CREATE
        public async Task<ActionResult<ActionType>> Create(ClaimsPrincipal claims, ActionType item)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var actionType = await _actionTypeRepository.FirstOrDefault(a => a.Name == item.Name);
            if (actionType != null)
                return new BadRequestObjectResult($"The action type named '{item.Name}' already exists.");
                    
            await _actionTypeRepository.Create(item);
            return new OkObjectResult(item);
        }
        #endregion CREATE

        #region GET
        public async Task<ActionResult<ActionType>> GetOne(
            ClaimsPrincipal claims, Guid id, 
            params Expression<Func<ActionType, object>>[] includeProperties)
        {
            var actionType = await _actionTypeRepository.GetById(id, includeProperties);

            if (actionType == null)
                return new NotFoundObjectResult(new ActionType {Id = id});

            if (includeProperties.Length == 0 || UserService.IsHasAccess(claims))
                return new OkObjectResult(actionType);

            var carIds = await _carService.GetCarsIdsByUserId(UserService.GetUserId(claims));
            var userActions = actionType.Actions.Where(a => carIds.Contains(a.CarId)).ToList();

            actionType.Actions = userActions;

            return new OkObjectResult(actionType);
        }

        public async Task<ActionResult<IEnumerable<ActionType>>> GetAll(
            ClaimsPrincipal claims = null,
            Expression<Func<ActionType, bool>> filter = null,
            Func<IQueryable<ActionType>, IOrderedQueryable<ActionType>> orderBy = null,
            params Expression<Func<ActionType, object>>[] includeProperties)
        {
            var actionTypes = await _actionTypeRepository.GetAll(filter, orderBy, includeProperties);

            if (includeProperties.Length == 0 || UserService.IsHasAccess(claims))
                return new OkObjectResult(actionTypes.ToList());

            var carIds = await _carService.GetCarsIdsByUserId(UserService.GetUserId(claims));
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
        public async Task<ActionResult<ActionType>> Update(ClaimsPrincipal claims, ActionType item)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var actionType = await _actionTypeRepository.FirstOrDefault(a => a.Name == item.Name);
            if (actionType != null)
                return new BadRequestObjectResult($"The action type named '{item.Name}' already exists.");

            actionType = await _actionTypeRepository.FirstOrDefault(a => a.Id == item.Id);
            if (actionType == null)
                return new NotFoundObjectResult(new ActionType {Id = item.Id});

            var actions = (await _actionTypeRepository.GetById(item.Id, a => a.Actions)).Actions;
            var actionTypeToCreate = new ActionType {Name = item.Name};

            await _actionTypeRepository.Create(actionTypeToCreate);

            foreach (var carAction in actions)
            {
                carAction.Type = actionTypeToCreate.Name;
                await _unitOfWork.CarActionRepository.Update(carAction);
            }

            await _actionTypeRepository.Remove(actionType);

            return new OkObjectResult(actionTypeToCreate);
        }
        #endregion UPDATE

        #region DELETE
        public async Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var actionType = await _actionTypeRepository.GetById(id, a => a.Actions);
            if (actionType == null)
                return new NotFoundObjectResult(new ActionType { Id = id });

            if (actionType.Actions.Count > 0)
                return new BadRequestObjectResult(
                    $"The '{actionType.Name}' type with id={{{id}}} cannot be deleted, because there are Actions with this type.");

            await _actionTypeRepository.Remove(actionType);

            return new NoContentResult();
        }
        #endregion DELETE

        #endregion CRUD


        public async Task<bool> IsActionTypeExist(string name)
        {
            var actionType = await _actionTypeRepository.FirstOrDefault(a => a.Name == name);
            return actionType != null;
        }
    }
}
