using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.Db.Repositories;

public class ReservationsRepository : IReservationsRepository
{
    private readonly RestaurantReservationDbContext _context;

    public ReservationsRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation> AddReservationAsync(Reservation reservation)
    {
        try
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateReservationAsync(int reservationId, Reservation reservation)
    {
        try
        {
            var existingReservation = await _context.Reservations.FindAsync(reservationId);
            if (existingReservation == null)
            {
                return false;
            }

            _context.Entry(existingReservation).CurrentValues.SetValues(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteReservationAsync(Reservation reservation)
    {
        try
        {
            var existingReservation = await _context.Reservations.FindAsync(reservation.ReservationID);
            if (existingReservation == null)
            {
                return false;
            }

            _context.Reservations.Remove(existingReservation);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Reservation?> GetReservationAsync(int reservationId)
    {
        try
        {
            return await _context.Reservations.FindAsync(reservationId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Reservation?>> GetReservationsAsync()
    {
        try
        {
            return await _context.Reservations.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Reservation>> GetReservationsByCustomerAsync(int customerId)
    {
        try
        {
            return await _context.Reservations.Where(r => r.CustomerID == customerId).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Reservation?> ListOrdersAndMenuItemsAsync(int reservationId)
    {
        try
        {
            return await _context.Reservations
                .Include(r => r.Orders)
                .ThenInclude(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .FirstOrDefaultAsync(r => r.ReservationID == reservationId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Reservation?>> GetCustomerReservationsAsync(int customerId)
    {
        try
        {
            return await _context.Reservations.Where(r => r.CustomerID == customerId).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}