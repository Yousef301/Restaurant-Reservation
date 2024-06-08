using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories;

public class MenuItemRepository
{
    private readonly RestaurantReservationDbContext _context;

    public MenuItemRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddMenuItemAsync(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();
        return "Menu item added successfully.";
    }

    public async Task<string> UpdateMenuItemAsync(MenuItem menuItem)
    {
        var existingMenuItem = await _context.MenuItems.FindAsync(menuItem.ItemID);
        if (existingMenuItem == null)
        {
            return "Menu item not found.";
        }

        _context.Entry(existingMenuItem).CurrentValues.SetValues(menuItem);
        await _context.SaveChangesAsync();
        return "Menu item updated successfully.";
    }

    public async Task<string> DeleteMenuItemAsync(int menuItemId)
    {
        var existingMenuItem = await _context.MenuItems.FindAsync(menuItemId);
        if (existingMenuItem == null)
        {
            return "Menu item not found.";
        }

        _context.MenuItems.Remove(existingMenuItem);
        await _context.SaveChangesAsync();
        return "Menu item deleted successfully.";
    }

    public async Task<MenuItem?> GetMenuItemAsync(int menuItemId)
    {
        return await _context.MenuItems.FindAsync(menuItemId);
    }

    public async Task<List<MenuItem>> ListOrderedMenuItemsAsync(int reservationId)
    {
        return await _context.OrderItems
            .Where(oi => oi.Order.ReservationID == reservationId)
            .Select(oi => oi.Item)
            .ToListAsync();
    }
}