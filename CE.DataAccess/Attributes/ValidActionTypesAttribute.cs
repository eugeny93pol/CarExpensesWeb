using System.ComponentModel.DataAnnotations;
using System.Linq;
using CE.DataAccess.Constants;

namespace CE.DataAccess.Attributes
{
    public class ValidActionTypesAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return ActionTypesConstants.Actions.Contains(value?.ToString());
        }
    }
}
