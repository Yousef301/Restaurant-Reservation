using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories;

public class OrderItemRepository
{
    private readonly RestaurantReservationDbContext _context;

    public OrderItemRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddOrderItem(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
        return "Order item added successfully.";
    }

    public async Task<string> UpdateOrderItem(OrderItem orderItem)
    {
        var existingOrderItem = await _context.OrderItems.FindAsync(orderItem.OrderItemID);
        if (existingOrderItem == null)
        {
            return "Order item not found.";
        }

        _context.Entry(existingOrderItem).CurrentValues.SetValues(orderItem);
        await _context.SaveChangesAsync();
        return "Order item updated successfully.";
    }

    public async Task<string> DeleteOrderItem(int orderItemId)
    {
        var existingOrderItem = await _context.OrderItems.FindAsync(orderItemId);
        if (existingOrderItem == null)
        {
            return "Order item not found.";
        }

        _context.OrderItems.Remove(existingOrderItem);
        await _context.SaveChangesAsync();
        return "Order item deleted successfully.";
    }

    public async Task<OrderItem?> GetOrderItem(int orderItemId)
    {
        return await _context.OrderItems.FindAsync(orderItemId);
    }
}