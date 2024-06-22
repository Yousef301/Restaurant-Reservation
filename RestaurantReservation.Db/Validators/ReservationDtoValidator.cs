using FluentValidation;
using RestaurantReservation.Db.DTOs;

namespace RestaurantReservation.Db.Validators;

public class ReservationDtoValidator : AbstractValidator<ReservationDto>
{
    public ReservationDtoValidator()
    {
        RuleFor(reservation => reservation.CustomerID).NotEmpty();
        RuleFor(reservation => reservation.RestaurantID).NotEmpty();
        RuleFor(reservation => reservation.TableID).NotEmpty();
        RuleFor(reservation => reservation.ReservationDate).NotEmpty().Must(x => x >= DateTime.Now);
        RuleFor(reservation => reservation.PartySize).NotEmpty().InclusiveBetween(1, 20);
    }
}