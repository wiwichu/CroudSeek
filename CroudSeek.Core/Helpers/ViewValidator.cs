using CroudSeek.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Helpers
{
    public class ViewValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as ViewForManipulationDto;
  
            if (model != null)
            {
                if (model.UserWeights != null
                    &&
                    model.UserWeights.Select((w) => w.UserId).Distinct().Count() != model.UserWeights.Count
                   )
                {
                    return new ValidationResult
                        ("There can only be one UserWeight per User");
                }
            }

            return ValidationResult.Success;

        }
    }
}
