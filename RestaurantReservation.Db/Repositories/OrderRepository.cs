using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;
using Serilog;

namespace RestaurantReservation.Db.Repositories;

public class OrderRepository : IOrdersRepository
{
    private readonly RestaurantReservationDbContext _context;

    public OrderRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<Order> AddOrderAsync(Order order)
    {
        try
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to add order");
            throw;
        }
    }

    public async Task<bool> UpdateOrderAsync(int id, Order order)
    {
        try
        {
            var existingOrder = await _context.Orders.FindAsync(order.OrderID);
            if (existingOrder == null)
            {
                return false;
            }

            _context.Entry(existingOrder).CurrentValues.SetValues(order);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to update order");
            throw;
        }
    }

    public async Task<bool> DeleteOrderAsync(Order order)
    {
        try
        {
            var existingOrder = await _context.Orders.FindAsync(order.OrderID);
            if (existingOrder == null)
            {
                return false;
            }

            _context.Orders.Remove(existingOrder);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to delete order");
            throw;
        }
    }

    public async Task<Order?> GetOrderAsync(int reservationId, int orderId)
    {
        try
        {
            return await _context.Orders
                .Where(o => o.ReservationID == reservationId && o.OrderID == orderId)
                .FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to get order");
            throw;
        }
    }

    public async Task<IEnumerable<Order?>> GetOrdersAsync(int reservationId)
    {
        try
        {
            return await _context.Orders.Where(o => o.ReservationID == reservationId).ToListAsync();
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to get orders");
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
            Log.Error(e, "Failed to save changes");
            throw;
        }
    }
}