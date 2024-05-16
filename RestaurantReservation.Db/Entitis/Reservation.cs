using System.ComponentModel.DataAnnotations;
using RestaurantReservation.Db.Validation;

namespace RestaurantReservation.Db.Entitis;

public class Reservation
{
    [Key] public int ReservationID { get; set; }
    [Required] public int CustomerID { get; set; }
    [Required] public int RestaurantID { get; set; }
    [Required] public int TableID { get; set; }
    [Required] [FutureDate] public DateTime ReservationDate { get; set; }
    [Required] [Range(1, 20)] public int PartySize { get; set; }
    public List<Order> Orders { get; set; }
}