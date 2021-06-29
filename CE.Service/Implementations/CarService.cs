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
    public class CarService : ICarService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly CarRepository _carRepository;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _carRepository = _unitOfWork?.CarRepository;
        }

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

        #region CRUD

        #region CREATE
        public async Task<ActionResult<Car>> Create(ClaimsPrincipal claims, Car item)
        {
            item.UserId = UserService.GetUserId(claims);
            await _carRepository.Create(item);
            await _unitOfWork.CarSettingsRepository.Create(new CarSettings(item.Id));
            return new OkObjectResult(item);
        }
        #endregion CREATE

        #region GET
        public async Task<ActionResult<Car>> GetOne(
            ClaimsPrincipal claims, 
            Guid id, 
            params Expression<Func<Car, object>>[] includeProperties)
        {
            var car = await _carRepository.GetById(id, includeProperties);
            if (!UserService.IsHasAccess(claims, id))
                return new ForbidResult();
            return car != null ? new OkObjectResult(car) : new NotFoundObjectResult(id);
        }

        public async Task<ActionResult<IEnumerable<Car>>> GetAll(
            ClaimsPrincipal claims = null, 
            Expression<Func<Car, bool>> filter = null, 
            Func<IQueryable<Car>, IOrderedQueryable<Car>> orderBy = null,
            params Expression<Func<Car, object>>[] includeProperties)
        {
            var cars = await _carRepository.GetAll(filter, orderBy, includeProperties);
            return new OkObjectResult(cars.ToList());
        }

        public async Task<ActionResult<IEnumerable<Car>>> GetCarsOfCurrentUser(
            ClaimsPrincipal claims,
            Func<IQueryable<Car>, IOrderedQueryable<Car>> orderBy = null,
            params Expression<Func<Car, object>>[] includeProperties)
        {
            var userId = UserService.GetUserId(claims);
            var cars = await _carRepository.GetAll(
                car => car.UserId == userId,
                orderBy, includeProperties);
            return new OkObjectResult(cars.ToList());
        }
        #endregion GET

        #region UPDATE
        public async Task<ActionResult<Car>> Update(ClaimsPrincipal claims, Car item)
        {
            var car = await _carRepository.FirstOrDefault(c => c.Id == item.Id);

            if (!(UserService.IsHasAccess(claims, item.UserId) &&
                  UserService.IsHasAccess(claims, car?.UserId)))
                return new ForbidResult();

            if (car == null)
                return new NotFoundObjectResult(item.Id);

            if (item.UserId == Guid.Empty)
                return new BadRequestObjectResult("The UserId field is required.");

            await _carRepository.Update(item);
            return new OkObjectResult(item);
        }
        
        public async Task<ActionResult<Car>> UpdatePartial(ClaimsPrincipal claims, Car item)
        {
            var car = await _carRepository.FirstOrDefault(c => c.Id == item.Id);
            if (car == null)
                return new NotFoundObjectResult(item.Id);

            car = UpdateCarInstance(car, item);
            return await Update(claims, car);
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

        #region PRIVATE STATIC
        private static Car UpdateCarInstance(Car savedCar, Car car)
        {
            savedCar.Brand = car.Brand ?? savedCar.Brand;
            savedCar.Model = car.Model ?? savedCar.Model;
            savedCar.Vin = car.Vin ?? savedCar.Vin;
            savedCar.Year = car.Year != 0 ? car.Year : savedCar.Year;
            savedCar.UserId = car.UserId != Guid.Empty ? car.UserId : savedCar.UserId;
            return savedCar;
        }
        #endregion PRIVATE STATIC
    }
}
