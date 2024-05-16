using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories;

public class OrderRepository
{
    private readonly RestaurantReservationDbContext _context;

    public OrderRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return "Order added successfully.";
    }

    public async Task<string> UpdateOrder(Order order)
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

    public async Task<string> DeleteOrder(int orderId)
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

    public async Task<Order?> GetOrder(int orderId)
    {
        return await _context.Orders.FindAsync(orderId);
    }
}