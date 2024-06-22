using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories.Interfaces;

public interface IMenuItemsRepository
{
    public Task<MenuItem> AddMenuItemAsync(MenuItem menuItem);
    public Task<bool> UpdateMenuItemAsync(int id, MenuItem menuItem);
    public Task<bool> DeleteMenuItemAsync(MenuItem menuItem);
    public Task<MenuItem?> GetMenuItemAsync(int menuItemId);
    public Task<List<MenuItem>> ListOrderedMenuItemsAsync(int reservationId);
    public Task SaveChangesAsync();
}