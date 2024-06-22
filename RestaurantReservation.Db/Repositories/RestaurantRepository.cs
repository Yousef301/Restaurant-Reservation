using RestaurantReservation.Db.Entities;
using Serilog;

namespace RestaurantReservation.Db.Repositories;

public class RestaurantRepository
{
    private readonly RestaurantReservationDbContext _context;

    public RestaurantRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<Restaurant> AddRestaurantAsync(Restaurant restaurant)
    {
        try
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }
        catch (Exception e)
        {
            Log.Error(e, "Error adding restaurant");
            throw;
        }
    }

    public async Task<bool> UpdateRestaurantAsync(Restaurant restaurant)
    {
        try
        {
            var existingRestaurant = await _context.Restaurants.FindAsync(restaurant.RestaurantID);
            if (existingRestaurant == null)
            {
                return false;
            }

            _context.Entry(existingRestaurant).CurrentValues.SetValues(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "Error updating restaurant");
            throw;
        }
    }

    public async Task<bool> DeleteRestaurantAsync(int restaurantId)
    {
        try
        {
            var existingRestaurant = await _context.Restaurants.FindAsync(restaurantId);
            if (existingRestaurant == null)
            {
                return false;
            }

            _context.Restaurants.Remove(existingRestaurant);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "Error deleting restaurant");
            throw;
        }
    }

    public async Task<Restaurant?> GetRestaurantAsync(int restaurantId)
    {
        try
        {
            return await _context.Restaurants.FindAsync(restaurantId);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error getting restaurant");
            throw;
        }
    }
}