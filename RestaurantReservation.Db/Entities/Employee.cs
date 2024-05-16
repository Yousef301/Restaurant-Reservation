using System.ComponentModel.DataAnnotations;
using RestaurantReservation.Db.Enums;

namespace RestaurantReservation.Db.Entities;

public class Employee
{
    [Key] public int EmployeeID { get; set; }
    [Required] public int RestaurantID { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(40)]
    public string LastName { get; set; }

    [Required] public Position Position { get; set; }

    public List<Order> Orders { get; set; }
}