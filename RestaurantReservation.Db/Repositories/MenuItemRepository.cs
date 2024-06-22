using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.Db.Repositories;

public class MenuItemRepository : IMenuItemsRepository
{
    private readonly RestaurantReservationDbContext _context;

    public MenuItemRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<MenuItem> AddMenuItemAsync(MenuItem menuItem)
    {
        try
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateMenuItemAsync(int menuItemId, MenuItem menuItem)
    {
        try
        {
            var existingMenuItem = await _context.MenuItems.FindAsync(menuItemId);
            if (existingMenuItem == null)
            {
                return false;
            }

            _context.Entry(existingMenuItem).CurrentValues.SetValues(menuItem);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteMenuItemAsync(MenuItem menuItem)
    {
        try
        {
            var existingMenuItem = await _context.MenuItems.FindAsync(menuItem.ItemID);
            if (existingMenuItem == null)
            {
                return false;
            }

            _context.MenuItems.Remove(existingMenuItem);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<MenuItem?> GetMenuItemAsync(int menuItemId)
    {
        try
        {
            return await _context.MenuItems.FindAsync(menuItemId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<MenuItem>> ListOrderedMenuItemsAsync(int reservationId)
    {
        try
        {
            return await _context.OrderItems
                .Where(oi => oi.Order.ReservationID == reservationId)
                .Select(oi => oi.Item)
                .ToListAsync();
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