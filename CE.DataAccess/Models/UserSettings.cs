﻿using CE.DataAccess.Attributes;
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

        public long? DefaultCarId { set; get; }

        public long UserId { get; set; }

        public UserSettings(long userId)
        {
            UserId = userId;
            Language = LanguagesConstants.English;
            Theme = ThemesConstants.Light;
            MeasurementSystem = MeasurementSystemsConstants.Metric;
        }
    }
}
