using System.ComponentModel.DataAnnotations;
using RestaurantReservation.Db.Enums;

namespace RestaurantReservation.Db.Entities;

public class Employee
{
    [Key] public int EmployeeID { get; set; }
    public int RestaurantID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Position Position { get; set; }

    public List<Order> Orders { get; set; }
}