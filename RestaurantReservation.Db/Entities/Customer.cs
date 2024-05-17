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

    [Required] [EmailAddress] public string Email { get; set; }

    [Required] [Phone] public string PhoneNumber { get; set; }

    public List<Reservation> Reservations { get; set; }
}