namespace RestaurantReservation.Db.DTOs;

public class ReservationDto
{
    public int CustomerID { get; set; }
    public int RestaurantID { get; set; }
    public int TableID { get; set; }
    public DateTime ReservationDate { get; set; }
    public int PartySize { get; set; }
}