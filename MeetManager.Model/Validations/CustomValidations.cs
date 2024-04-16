using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace MeetManager.Model
{
    internal class EndDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // get start date
            DateTime? startDate = (DateTime?)validationContext.ObjectType.GetProperty("StartDate")!.GetValue(validationContext.ObjectInstance);
            if (startDate == null) // checking this in case startDate is null...we don't that to flag the end date validation
                return ValidationResult.Success;

            if (((DateTime?)value!).Value.RemoveMilliseconds() > startDate.Value.RemoveMilliseconds())
                return ValidationResult.Success;

            return new ValidationResult("End date must be greater than the start date.");

        }
    }

    internal class RegistrationDeadline : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // get start date
            DateTime? startDate = (DateTime?)validationContext.ObjectType.GetProperty("StartDate")!.GetValue(validationContext.ObjectInstance);
            if (startDate == null) // checking this in case startDate is null...we don't that to flag the registration deadline validation
                return ValidationResult.Success;

            TimeSpan diff = (TimeSpan)(startDate.Value.RemoveMilliseconds() - ((DateTime?)value!).Value.RemoveMilliseconds());

            if (diff.TotalHours >= 24)
                return ValidationResult.Success;


            return new ValidationResult("Registration Deadline must be at least 24 hours before start date.");
        }
    }
}
