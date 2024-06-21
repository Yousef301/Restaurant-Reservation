using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities;

public class MenuItem
{
    [Key] public int ItemID { get; set; }
    public int RestaurantID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}