using FluentValidation;
using RestaurantReservation.Db.DTOs;

namespace RestaurantReservation.Db.Validators;

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(orderItem => orderItem.OrderID).NotEmpty();
        RuleFor(orderItem => orderItem.ItemID).NotEmpty();
        RuleFor(orderItem => orderItem.Quantity).NotEmpty().InclusiveBetween(1, 1000);
    }
}