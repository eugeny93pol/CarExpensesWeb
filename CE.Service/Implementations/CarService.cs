using CE.DataAccess.Dtos;
using CE.DataAccess.Models;
using CE.Repository;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CE.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IGenericRepository<Car> _carRepository;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _carRepository = _unitOfWork?.CarRepository;
        }

        #region CRUD

        #region CREATE
        public async Task<ActionResult<CarDto>> Create(ClaimsPrincipal claims, CreateCarDto dto)
        {
            if (!UserService.IsHasAccess(claims, dto.UserId))
                return new ForbidResult();

            var car = dto.AsDbModel();

            await _carRepository.Create(car);
            await _unitOfWork.CarSettingsRepository.Create(new CarSettings(car.Id));
            return new OkObjectResult(car.AsDto());
        }
        #endregion CREATE

        #region GET
        public async Task<ActionResult<CarDto>> GetOne(ClaimsPrincipal claims, Guid id,
            params Expression<Func<Car, object>>[] includeProperties)
        {
            var car = await _carRepository.GetById(id, includeProperties);

            if (!await IsUserHasAccessToCar(claims, id, car))
                return new ForbidResult();

            return car != null ? new OkObjectResult(car.AsDto()) : new NotFoundObjectResult(id);
        }

        public async Task<ActionResult<IEnumerable<CarDto>>> GetAll(ClaimsPrincipal claims,
            Expression<Func<Car, bool>> filter = null,
            Func<IQueryable<Car>, IOrderedQueryable<Car>> orderBy = null,
            params Expression<Func<Car, object>>[] includeProperties)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var cars = await _carRepository.GetAll(filter, orderBy, includeProperties);
            return new OkObjectResult(ConvertToDtos(cars));
        }

        public async Task<ActionResult<IEnumerable<CarDto>>> GetCarsOfCurrentUser(
            ClaimsPrincipal claims,
            Func<IQueryable<Car>, IOrderedQueryable<Car>> orderBy = null,
            params Expression<Func<Car, object>>[] includeProperties)
        {
            var userId = UserService.GetUserId(claims);
            var cars = await _carRepository.GetAll(
                car => car.UserId == userId,
                orderBy, includeProperties);
            return new OkObjectResult(ConvertToDtos(cars));
        }
        #endregion GET

        #region UPDATE
        public async Task<ActionResult> Update(ClaimsPrincipal claims, UpdateCarDto item)
        {
            var car = await _carRepository.FirstOrDefault(c => c.Id == item.Id);

            if (item.UserId == Guid.Empty)
                return new BadRequestObjectResult("The UserId field is required.");

            if (!(UserService.IsHasAccess(claims, item.UserId) &&
                  UserService.IsHasAccess(claims, car?.UserId)))
                return new ForbidResult();

            if (car == null)
                return new NotFoundObjectResult(item.Id);

            var lastMileage = (await _unitOfWork.CarActionRepository.Actions.GetAll(
                action => action.CarId == item.Id)).Max(a => a.Mileage);

            if (item.Mileage < lastMileage)
                return new BadRequestObjectResult("The mileage is incorrect.");

            if (item.Mileage > lastMileage)
                await _unitOfWork.CarActionRepository.Mileages.Create(
                    new CarActionMileage
                    {
                        CarId = item.Id,
                        Date = DateTime.UtcNow,
                        Mileage = item.Mileage
                    });

            await _carRepository.Update(item.AsDbModel());
            return new NoContentResult();
        }
        #endregion UPDATE

        #region DELETE
        public async Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id)
        {
            var car = await _carRepository.GetById(id);
            if (!UserService.IsHasAccess(claims, car?.UserId))
                return new ForbidResult();
            if (car == null)
                return new NotFoundObjectResult(id);
            await _carRepository.Remove(car);
            return new NoContentResult();
        }
        #endregion DELETE

        #endregion CRUD

        #region HELPERS
        public async Task<Guid[]> GetCarsIdsOfCurrentUser(ClaimsPrincipal claims)
        {
            var userId = UserService.GetUserId(claims);
            var cars = await _carRepository.GetAll(c => c.UserId == userId);
            return cars.Select(c => c.Id).ToArray();
        }

        public async Task<bool> IsUserHasAccessToCar(ClaimsPrincipal claims, Guid carId, Car car = null)
        {
            car ??= await _carRepository.GetById(carId);
            return UserService.IsHasAccess(claims, car?.UserId);
        }
        #endregion HELPERS

        #region PRIVATE STATIC
        private static IEnumerable<CarDto> ConvertToDtos(IEnumerable<Car> cars)
        {
            return cars.ToList().Select(car => car.AsDto());
        }
        #endregion PRIVATE STATIC
    }
}
