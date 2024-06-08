using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories;

public class OrderRepository
{
    private readonly RestaurantReservationDbContext _context;

    public OrderRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return "Order added successfully.";
    }

    public async Task<string> UpdateOrderAsync(Order order)
    {
        var existingOrder = await _context.Orders.FindAsync(order.OrderID);
        if (existingOrder == null)
        {
            return "Order not found.";
        }

        _context.Entry(existingOrder).CurrentValues.SetValues(order);
        await _context.SaveChangesAsync();
        return "Order updated successfully.";
    }

    public async Task<string> DeleteOrderAsync(int orderId)
    {
        var existingOrder = await _context.Orders.FindAsync(orderId);
        if (existingOrder == null)
        {
            return "Order not found.";
        }

        _context.Orders.Remove(existingOrder);
        await _context.SaveChangesAsync();
        return "Order deleted successfully.";
    }

    public async Task<Order?> GetOrderAsync(int orderId)
    {
        return await _context.Orders.FindAsync(orderId);
    }
}