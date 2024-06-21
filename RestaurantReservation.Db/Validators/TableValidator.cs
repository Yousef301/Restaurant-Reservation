using FluentValidation;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Validators;

public class TableValidator : AbstractValidator<Table>
{
    public TableValidator()
    {
        RuleFor(table => table.RestaurantID).NotEmpty();
        RuleFor(table => table.Capacity).InclusiveBetween(1, 20);
    }
}