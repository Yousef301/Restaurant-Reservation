using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Validation;

public class FutureDateAttribute : ValidationAttribute
{
    public FutureDateAttribute()
        : base("The {0} must be a date in the future.")
    {
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateTime)
        {
            if (dateTime > DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
        }

        return new ValidationResult("Invalid date format");
    }
}