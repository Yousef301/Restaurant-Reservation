using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entitis;

public class Order
{
    [Key] public int OrderID { get; set; }
    [Required] public int ReservationID { get; set; }
    [Required] public int EmployeeID { get; set; }
    [Required] public DateTime OrderDate = DateTime.Now;
    [Required] [Range(0.01, 10000)] public double TotalAmount { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}