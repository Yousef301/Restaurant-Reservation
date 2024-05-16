using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entitis;

public class Table
{
    [Key] public int TableID { get; set; }
    [Required] public int RestaurantID { get; set; }
    [Range(1, 20)] public int Capacity { get; set; }
    public List<Reservation> Reservations { get; set; }
}