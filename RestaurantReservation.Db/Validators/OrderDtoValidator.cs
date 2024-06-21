using FluentValidation;
using RestaurantReservation.Db.DTOs;

namespace RestaurantReservation.Db.Validators;

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator()
    {
        RuleFor(order => order.ReservationID).NotEmpty();
        RuleFor(order => order.EmployeeID).NotEmpty();
        RuleFor(order => order.OrderDate).NotEmpty();
        RuleFor(order => order.TotalAmount).NotEmpty().InclusiveBetween(0.01m, 1000m);
    }
}