using CE.DataAccess.Constants;

namespace CE.DataAccess
{
    public class CarSettings : BaseEntity
    {
        public string MeasurementSystem { get; set; }

        //Navigation
        public long CarId { get; set; }

        public CarSettings(long carId)
        {
            MeasurementSystem = MeasurementSystemConstants.km;
            CarId = carId;
        }
    }
}
