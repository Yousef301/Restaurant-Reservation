using FluentValidation;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Validators;

public class ReservationValidator : AbstractValidator<Reservation>
{
    public ReservationValidator()
    {
        RuleFor(reservation => reservation.CustomerID).NotEmpty();
        RuleFor(reservation => reservation.RestaurantID).NotEmpty();
        RuleFor(reservation => reservation.TableID).NotEmpty();
        RuleFor(reservation => reservation.ReservationDate).NotEmpty().Must(x => x > DateTime.Now);
        RuleFor(reservation => reservation.PartySize).NotEmpty().InclusiveBetween(1, 20);
    }
}