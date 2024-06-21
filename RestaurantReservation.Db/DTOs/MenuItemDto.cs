namespace RestaurantReservation.Db.DTOs;

public class MenuItemDto
{
    public int RestaurantID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}