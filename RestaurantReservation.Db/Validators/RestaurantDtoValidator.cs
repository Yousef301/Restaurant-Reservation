using FluentValidation;
using RestaurantReservation.Db.DTOs;

namespace RestaurantReservation.Db.Validators;

public class RestaurantDtoValidator : AbstractValidator<RestaurantDto>
{
public RestaurantDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, 50);
        RuleFor(x => x.Address).NotEmpty().Length(1, 100);
        RuleFor(x => x.PhoneNumber).NotEmpty().Length(1, 20).Matches(@"^\d{3}-\d{3}-\d{4}$");
        RuleFor(x => x.OperatingHours).NotEmpty().Length(5, 50);
    }
}