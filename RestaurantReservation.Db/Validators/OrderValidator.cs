using FluentValidation;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(order => order.ReservationID).NotEmpty();
        RuleFor(order => order.EmployeeID).NotEmpty();
        RuleFor(order => order.OrderDate).NotEmpty();
        RuleFor(order => order.TotalAmount).NotEmpty().InclusiveBetween(0.01m, 1000m);
    }
}