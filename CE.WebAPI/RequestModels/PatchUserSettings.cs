using System;
using CE.DataAccess;
using CE.DataAccess.Attributes;

namespace CE.WebAPI.RequestModels
{
    public class PatchUserSettings
    {
        [ValidLanguages]
        public string Language { get; set; }
        public string Theme { get; set; }
        [ValidMeasurementSystems]
        public string MeasurementSystem { get; set; }
        public Guid? DefaultCarId { get; set; }

        public UserSettings GetUserSettings()
        {
            return new()
            {
                Language = this.Language,
                Theme = this.Theme,
                MeasurementSystem = this.MeasurementSystem,
                DefaultCarId = this.DefaultCarId
            };
        }
    }
}
