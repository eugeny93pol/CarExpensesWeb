﻿using CE.DataAccess.Constants;

namespace CE.DataAccess
{
    public class CarSettings : BaseEntity
    {
        public string MeasurementSystem { get; set; }

        public long CarId { get; set; }

        public CarSettings(long carId)
        {
            MeasurementSystem = MeasurementSystemsConstants.km;
            CarId = carId;
        }
    }
}
