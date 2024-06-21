using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities;

public class Table
{
    [Key] public int TableID { get; set; }
    public int RestaurantID { get; set; }
    public int Capacity { get; set; }
    public List<Reservation> Reservations { get; set; }
}