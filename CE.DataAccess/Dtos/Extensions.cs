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
                Cars = user.Cars,
                Settings = user.Settings
            };
        }
        #endregion USER'S DTOS

        public static UserSettings AsDbModel(this UserSettingsDto dto)
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

        public static UserSettingsDto AsDto(this UserSettings settings)
        {
            return new UserSettingsDto
            {
                Id = settings.Id,
                UserId = settings.UserId,
                DefaultCarId = settings.DefaultCarId,
                Language = settings.Language,
                MeasurementSystem = settings.MeasurementSystem,
                Theme = settings.Theme
            };
        }

    }
}