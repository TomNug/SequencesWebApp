using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace SequencesWebApp.Models.Validators
{
    public class AtLeastOneIntegerElement : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult("Value cannot be null");
            }
            var valueAsIntList = value as List<int>;
            if (valueAsIntList == null)
            {
                // May have failed to cast to List<int>
                return new ValidationResult("Sequences should contain at least one integer");
            }
            if (valueAsIntList.Count == 0)
            {
                return new ValidationResult("Sequences should contain at least one integer");
            }

            return ValidationResult.Success;
        }
    }
}


