using RestaurantReservation.Db.Enums;

namespace RestaurantReservation.Db.DTOs;

public class EmployeeDto
{
    public int RestaurantID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Position Position { get; set; }
}