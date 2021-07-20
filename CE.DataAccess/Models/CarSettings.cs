using System;
using CE.DataAccess.Constants;

namespace CE.DataAccess.Models
{
    public class CarSettings : BaseEntity
    {
        public DistanceUnit DistanceUnit { get; set; }
        
        public FuelVolumeUnit FuelVolumeUnit { get; set; }

        public FuelConsumptionType FuelConsumptionType { get; set; }
        
        public Guid CarId { get; set; }

        public CarSettings(Guid carId)
        {
            DistanceUnit = DistanceUnit.Kilometer;
            FuelVolumeUnit = FuelVolumeUnit.Liter;
            FuelConsumptionType = FuelConsumptionType.VolumePer100DistanceUnits;
            CarId = carId;
        }
    }
}
