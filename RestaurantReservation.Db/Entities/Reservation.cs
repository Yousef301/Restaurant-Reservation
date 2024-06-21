using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities;

public class Reservation
{
    [Key] public int ReservationID { get; set; }
    public int CustomerID { get; set; }
    public int RestaurantID { get; set; }
    public int TableID { get; set; }
    public DateTime ReservationDate { get; set; }
    public int PartySize { get; set; }
    public List<Order> Orders { get; set; }
}