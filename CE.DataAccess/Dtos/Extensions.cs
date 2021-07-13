using System.Linq;
using CE.DataAccess.Models;

namespace CE.DataAccess.Dtos
{
    public static class Extensions
    {
        #region USER'S DTOS
        public static User AsDbModel(this CreateUserDto dto)
        {
            return new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password
            };
        }
        
        public static User AsDbModel(this UpdateUserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Role = dto.Role,
                Password = dto.Password
            };
        }

        public static UserDto AsDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role,
                Cars = user.Cars.Select(car => car.AsDto()).ToList(),
                Settings = user.Settings.AsDto()
            };
        }
        #endregion USER'S DTOS

        #region CAR'S DTOS

        public static Car AsDbModel(this CreateCarDto dto)
        {
            return new Car
            {
                UserId = dto.UserId,
                Brand = dto.Brand,
                Model = dto.Model,
                Mileage = dto.Mileage,
                Year = dto.Year,
                Vin = dto.Vin
            };
        }

        public static Car AsDbModel(this UpdateCarDto dto)
        {
            return new Car
            {
                Id = dto.Id,
                UserId = dto.UserId,
                Brand = dto.Brand,
                Model = dto.Model,
                Mileage = dto.Mileage,
                Year = dto.Year,
                Vin = dto.Vin
            };
        }

        public static CarDto AsDto(this Car car)
        {
            return new CarDto
            {
                Id = car.Id,
                UserId = car.UserId,
                Brand = car.Brand,
                Model = car.Model,
                Mileage = car.Mileage,
                Year = car.Year,
                Vin = car.Vin,
                Actions = car.Actions,
                Settings = car.Settings
            };
        }
        #endregion CAR'S DTOS

        #region USER SETTINGS DTOS
        public static UserSettings AsDbModel(this CreateUserSettingsDto dto)
        {
            return new UserSettings
            {
                UserId = dto.UserId,
                DefaultCarId = dto.DefaultCarId,
                Language = dto.Language,
                MeasurementSystem = dto.MeasurementSystem,
                Theme = dto.Theme
            };
        }

        public static UserSettings AsDbModel(this UpdateUserSettingsDto dto)
        {
            return new UserSettings
            {
                Id = dto.Id,
                UserId = dto.UserId,
                DefaultCarId = dto.DefaultCarId,
                Language = dto.Language,
                MeasurementSystem = dto.MeasurementSystem,
                Theme = dto.Theme
            };
        }

        public static GetUserSettingsDto AsDto(this UserSettings settings)
        {
            if (settings is null)
                return null;

            return new GetUserSettingsDto
            {
                Id = settings.Id,
                UserId = settings.UserId,
                DefaultCarId = settings.DefaultCarId,
                Language = settings.Language,
                MeasurementSystem = settings.MeasurementSystem,
                Theme = settings.Theme
            };
        }
        #endregion USER SETTINGS DTOS
    }
}