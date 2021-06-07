using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CE.DataAccess.Attributes
{
    public class ValidValuesAttribute : ValidationAttribute
    {
        private readonly string[] _acceptValues;

        public ValidValuesAttribute(string[] values)
        {
            _acceptValues = values;
        }

        public override bool IsValid(object value)
        {
            return _acceptValues.Contains(value?.ToString());
        }
    }
}
