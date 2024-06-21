using FluentValidation;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Validators;

public class MenuItemValidator : AbstractValidator<MenuItem>
{
    public MenuItemValidator()
    {
        RuleFor(menuItem => menuItem.RestaurantID).NotEmpty();
        RuleFor(menuItem => menuItem.Name).NotEmpty().Length(3, 40);
        RuleFor(menuItem => menuItem.Description).NotEmpty().Length(1, 80);
        RuleFor(menuItem => menuItem.Price).NotEmpty().InclusiveBetween(0.01, 1000);
    }
}