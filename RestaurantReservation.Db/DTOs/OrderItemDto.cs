using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.DTOs;

public class OrderItemDto
{
    public int OrderID { get; set; }
    public int ItemID { get; set; }
    public int Quantity { get; set; }
    [Required] [ForeignKey("OrderID")] public Order Order { get; set; } = null!;
    [Required] [ForeignKey("ItemID")] public MenuItem Item { get; set; } = null!;
}