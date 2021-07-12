using CE.DataAccess.Models;

namespace CE.DataAccess.Dtos
{
    public static class Extensions
    {
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