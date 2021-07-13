using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess.Dtos;
using CE.DataAccess.Models;
using CE.Repository;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Implementations
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly IGenericRepository<UserSettings> _userSettingsRepository;
        private readonly ICarService _carService;

        public UserSettingsService(IUnitOfWork unitOfWork, ICarService carService)
        {
            _userSettingsRepository = (unitOfWork as UnitOfWork)?.UserSettingsRepository;
            _carService = carService;
        }

        #region CREATE
        public async Task<GetUserSettingsDto> Create(Guid userId)
        {
            var settings = await _userSettingsRepository.Create(new UserSettings(userId));
            return settings.AsDto();
        }

        public async Task<ActionResult<GetUserSettingsDto>> Create(ClaimsPrincipal claims, CreateUserSettingsDto dto)
        {
            if (!UserService.IsHasAccess(claims, dto.UserId))
                return new ForbidResult();
            
            var existingSettings = await _userSettingsRepository.FirstOrDefault(s => s.UserId == dto.UserId);

            if (existingSettings != null)
                return new BadRequestObjectResult("The user already has settings.");

            var settings = dto.AsDbModel();
            await _userSettingsRepository.Create(settings);

            return new OkObjectResult(settings.AsDto());
        }
        #endregion CREATE

        #region GET
        public async Task<ActionResult<GetUserSettingsDto>> GetOne(ClaimsPrincipal claims)
        {
            var userId = UserService.GetUserId(claims);

            var settings = (await GetSettingsByUserId(userId)).AsDto() ?? await Create(userId);

            return new OkObjectResult(settings);
        }

        public async Task<ActionResult<GetUserSettingsDto>> GetOne(ClaimsPrincipal claims, Guid id)
        {
            var settings = await _userSettingsRepository.GetById(id);

            var checkAccessResult = CheckAccessToSettings(claims, settings);
            return checkAccessResult ?? new OkObjectResult(settings.AsDto());
        }

        public async Task<ActionResult<IEnumerable<GetUserSettingsDto>>> GetAll(ClaimsPrincipal claims,
            Expression<Func<UserSettings, bool>> filter = null,
            Func<IQueryable<UserSettings>, IOrderedQueryable<UserSettings>> orderBy = null,
            params Expression<Func<UserSettings, object>>[] includeProperties)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var settings = await _userSettingsRepository.GetAll(filter, orderBy, includeProperties);
            return new OkObjectResult(settings.Select(s => s.AsDto()));
        }

        #endregion GET

        #region UPDATE
        public async Task<ActionResult> Update(ClaimsPrincipal claims, UpdateUserSettingsDto dto)
        {
            var settings = dto.AsDbModel();
            var checkResult = await CheckBeforeUpdate(claims, settings);

            if (checkResult != null)
                return checkResult;

            await _userSettingsRepository.Update(settings);

            return new NoContentResult();
        }
        #endregion UPDATE

        #region DELETE
        public async Task<ActionResult> Delete(ClaimsPrincipal claims, Guid id)
        {
            var settings = await _userSettingsRepository.GetById(id);
            
            var checkAccessResult = CheckAccessToSettings(claims, settings);
            if (checkAccessResult != null)
                return checkAccessResult;

            await _userSettingsRepository.Remove(settings);

            return new NoContentResult();
        }
        #endregion DELETE

        #region PRIVATE
        private async Task<UserSettings> GetSettingsByUserId(Guid userId)
        {
            return await _userSettingsRepository.FirstOrDefault(s => s.UserId == userId);
        }
        
        private static ActionResult CheckAccessToSettings(ClaimsPrincipal claims, UserSettings item)
        {
            if (item == null)
                return new NotFoundObjectResult("Settings with provided ID not found.");

            return !UserService.IsHasAccess(claims, item.UserId) ? new ForbidResult() : null;
        }

        private async Task<ActionResult> CheckAccessToCar(ClaimsPrincipal claims, UserSettings item)
        {
            if (item.DefaultCarId != null && !await _carService.IsUserHasAccessToCar(claims, (Guid)item.DefaultCarId))
                return new ForbidResult();

            return null;
        }

        private async Task<ActionResult> CheckBeforeUpdate(ClaimsPrincipal claims, UserSettings item)
        {
            var saved = await _userSettingsRepository.FirstOrDefault(s => s.Id == item.Id);

            var checkAccessResult = await CheckAccessToCar(claims, item);
            if (checkAccessResult != null)
                return checkAccessResult;

            return saved.UserId != item.UserId 
                ? new BadRequestObjectResult("The provided userID does not match the saved settings userID.") 
                : CheckAccessToSettings(claims, saved);
        }
        #endregion PRIVATE
    }
}
