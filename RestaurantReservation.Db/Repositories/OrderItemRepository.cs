using RestaurantReservation.Db.Entities;
using Serilog;

namespace RestaurantReservation.Db.Repositories;

public class OrderItemRepository
{
    private readonly RestaurantReservationDbContext _context;

    public OrderItemRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
    {
        try
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to add order item");
            throw;
        }
    }

    public async Task<bool> UpdateOrderItemAsync(OrderItem orderItem)
    {
        try
        {
            var existingOrderItem = await _context.OrderItems.FindAsync(orderItem.OrderItemID);
            if (existingOrderItem == null)
            {
                return false;
            }

            _context.Entry(existingOrderItem).CurrentValues.SetValues(orderItem);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to update order item");
            throw;
        }
    }

    public async Task<bool> DeleteOrderItemAsync(int orderItemId)
    {
        try
        {
            var existingOrderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (existingOrderItem == null)
            {
                return false;
            }

            _context.OrderItems.Remove(existingOrderItem);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OrderItem?> GetOrderItemAsync(int orderItemId)
    {
        try
        {
            return await _context.OrderItems.FindAsync(orderItemId);
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to get order item");
            throw;
        }
    }
}