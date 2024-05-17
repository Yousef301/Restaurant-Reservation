using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Db.Entities;

public class OrderItem
{
    [Key] public int OrderItemID { get; set; }
    [Required] public int OrderID { get; set; }
    [Required] public int ItemID { get; set; }
    [Required] [Range(1, 1000)] public int Quantity { get; set; }
    [Required] [ForeignKey("OrderID")] public Order Order { get; set; } = null!;
    [Required] [ForeignKey("ItemID")] public MenuItem Item { get; set; } = null!;
}