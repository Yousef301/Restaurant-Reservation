using FluentValidation;
using RestaurantReservation.Db.DTOs;
using RestaurantReservation.Db.Enums;

namespace RestaurantReservation.Db.Validators;

public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
{
    public EmployeeDtoValidator()
    {
        RuleFor(employee => employee.RestaurantID).NotEmpty();
        RuleFor(employee => employee.FirstName).NotEmpty().Length(3, 30);
        RuleFor(employee => employee.LastName).NotEmpty().Length(3, 40);
        RuleFor(employee => employee.Position).NotEmpty().IsInEnum().WithMessage(
            $"Position must be one of the following values: {string.Join(", ", Enum.GetNames(typeof(Position)))}");
        ;
    }
}