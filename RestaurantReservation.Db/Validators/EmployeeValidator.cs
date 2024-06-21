using FluentValidation;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Validators;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(employee => employee.RestaurantID).NotEmpty();
        RuleFor(employee => employee.FirstName).NotEmpty().Length(3, 30);
        RuleFor(employee => employee.LastName).NotEmpty().Length(3, 40);
        RuleFor(employee => employee.Position).NotEmpty();
    }
}