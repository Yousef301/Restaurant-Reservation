using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities;

public class Customer
{
    [Key] public int CustomerID { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(40)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; }

    [Required] [Phone] [MaxLength(20)] public string PhoneNumber { get; set; }

    public List<Reservation> Reservations { get; set; }
}