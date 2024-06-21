using FluentValidation;
using RestaurantReservation.Db.DTOs;

namespace RestaurantReservation.Db.Validators;

public class TableDtoValidator : AbstractValidator<TableDto>
{
    public TableDtoValidator()
    {
        RuleFor(table => table.RestaurantID).NotEmpty();
        RuleFor(table => table.Capacity).InclusiveBetween(1, 20);
    }
}