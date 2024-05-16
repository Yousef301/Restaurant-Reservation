using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories;

public class ReservationRepository
{
    private readonly RestaurantReservationDbContext _context;

    public ReservationRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddReservation(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        return "Reservation added successfully.";
    }

    public async Task<string> UpdateReservation(Reservation reservation)
    {
        var existingReservation = await _context.Reservations.FindAsync(reservation.ReservationID);
        if (existingReservation == null)
        {
            return "Reservation not found.";
        }

        _context.Entry(existingReservation).CurrentValues.SetValues(reservation);
        await _context.SaveChangesAsync();
        return "Reservation updated successfully.";
    }

    public async Task<string> DeleteReservation(int reservationId)
    {
        var existingReservation = await _context.Reservations.FindAsync(reservationId);
        if (existingReservation == null)
        {
            return "Reservation not found.";
        }

        _context.Reservations.Remove(existingReservation);
        await _context.SaveChangesAsync();
        return "Reservation deleted successfully.";
    }

    public async Task<Reservation?> GetReservation(int reservationId)
    {
        return await _context.Reservations.FindAsync(reservationId);
    }

    public async Task<List<Reservation>> GetReservationsByCustomer(int customerId)
    {
        return await _context.Reservations.Where(r => r.CustomerID == customerId).ToListAsync();
    }

    public async Task<Reservation?> ListOrdersAndMenuItems(int reservationId)
    {
        return await _context.Reservations
            .Include(r => r.Orders)
            .ThenInclude(o => o.OrderItems)
            .ThenInclude(oi => oi.Item)
            .FirstOrDefaultAsync(r => r.ReservationID == reservationId);
    }
}