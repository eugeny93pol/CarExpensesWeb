using System;
using CE.DataAccess.Constants;

namespace CE.DataAccess.Models
{
    public class CarSettings : BaseEntity
    {
        public string MeasurementSystem { get; set; }

        public Guid CarId { get; set; }

        public CarSettings(Guid carId)
        {
            MeasurementSystem = MeasurementSystemsConstants.km;
            CarId = carId;
        }
    }
}
