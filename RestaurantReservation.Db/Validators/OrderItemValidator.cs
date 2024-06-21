using FluentValidation;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Validators;

public class OrderItemValidator : AbstractValidator<OrderItem>
{
    public OrderItemValidator()
    {
        RuleFor(orderItem => orderItem.OrderID).NotEmpty();
        RuleFor(orderItem => orderItem.ItemID).NotEmpty();
        RuleFor(orderItem => orderItem.Quantity).NotEmpty().InclusiveBetween(1, 1000);
    }
}