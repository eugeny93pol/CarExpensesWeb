using System;
using CE.DataAccess.Attributes;
using CE.DataAccess.Constants;

namespace CE.DataAccess
{
    public class UserSettings : BaseEntity
    {
        [ValidLanguages]
        public string Language { get; set; }

        public string Theme { get; set; }

        [ValidMeasurementSystems]
        public string MeasurementSystem { set; get; }

        public Guid? DefaultCarId { set; get; }

        public Guid UserId { get; set; }

        public UserSettings()
        {
        }

        public UserSettings(Guid userId)
        {
            UserId = userId;
            Language = LanguagesConstants.English;
            Theme = ThemesConstants.Light;
            MeasurementSystem = MeasurementSystemsConstants.Metric;
        }
    }
}
