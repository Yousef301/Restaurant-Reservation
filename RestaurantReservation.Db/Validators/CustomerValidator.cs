using FluentValidation;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Validators;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.FirstName).NotEmpty().Length(3, 30);
        RuleFor(customer => customer.LastName).NotEmpty().Length(3, 40);
        RuleFor(customer => customer.Email).NotEmpty().EmailAddress().Length(1, 256);
        RuleFor(customer => customer.PhoneNumber).NotEmpty().Length(1, 20);
    }
}