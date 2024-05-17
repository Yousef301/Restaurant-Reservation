using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities;

public class MenuItem
{
    [Key] public int ItemID { get; set; }
    [Required] public int RestaurantID { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(40)]
    public string Name { get; set; }

    [Required] [MaxLength(80)] public string Description { get; set; }

    [Required] [Range(0.01, 1000)] public double Price { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}