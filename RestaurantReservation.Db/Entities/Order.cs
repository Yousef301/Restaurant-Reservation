using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities;

public class Order
{
    [Key] public int OrderID { get; set; }
    public int ReservationID { get; set; }
    public int EmployeeID { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}