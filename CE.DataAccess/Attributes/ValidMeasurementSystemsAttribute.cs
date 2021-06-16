using System.ComponentModel.DataAnnotations;
using System.Linq;
using CE.DataAccess.Constants;

namespace CE.DataAccess.Attributes
{
    public class ValidMeasurementSystemsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return MeasurementSystemsConstants.Systems.Contains(value?.ToString());
        }
    }
}
