using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories.Interfaces;

public interface IOrdersRepository
{
    public Task<Order> AddOrderAsync(Order order);
    public Task<bool> UpdateOrderAsync(int id, Order order);
    public Task<bool> DeleteOrderAsync(Order order);
    public Task<Order?> GetOrderAsync(int reservationId, int orderId);
    public Task<IEnumerable<Order?>> GetOrdersAsync(int reservationId);
    public Task SaveChangesAsync();
}