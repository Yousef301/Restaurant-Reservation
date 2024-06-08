using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities;

public class Order
{
    [Key] public int OrderID { get; set; }
    [Required] public int ReservationID { get; set; }
    [Required] public int EmployeeID { get; set; }
    [Required] public DateTime OrderDate { get; set; } = DateTime.Now;
    [Required] [Range(0.01, 10000)] public decimal TotalAmount { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}