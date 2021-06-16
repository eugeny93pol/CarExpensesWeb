using System.ComponentModel.DataAnnotations;
using System.Linq;
using CE.DataAccess.Constants;

namespace CE.DataAccess.Attributes
{
    public class ValidLanguagesAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return LanguagesConstants.Languages.Contains(value?.ToString());
        }
    }
}
