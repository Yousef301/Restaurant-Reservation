using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories.Interfaces;

public interface IReservationsRepository
{
    public Task<Reservation> AddReservationAsync(Reservation reservation);
    public Task<bool> UpdateReservationAsync(int id, Reservation reservation);
    public Task<bool> DeleteReservationAsync(Reservation reservation);
    public Task<Reservation?> GetReservationAsync(int reservationId);
    public Task<IEnumerable<Reservation?>> GetReservationsAsync();
    public Task<IEnumerable<Reservation?>> GetCustomerReservationsAsync(int customerId);
    public Task SaveChangesAsync();
}