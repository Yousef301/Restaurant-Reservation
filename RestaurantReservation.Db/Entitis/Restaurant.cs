using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entitis;

public class Restaurant
{
    [Key] public int RestaurantID { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string Address { get; set; }

    [Required] [Phone] public string PhoneNumber { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    public string OperatingHours { get; set; }

    public List<Table> Tables { get; set; }
    public List<Reservation> Reservations { get; set; }
    public List<Employee> Employees { get; set; }
    public List<MenuItem> MenuItems { get; set; }
}