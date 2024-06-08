using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories;

public class RestaurantRepository
{
    private readonly RestaurantReservationDbContext _context;

    public RestaurantRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddRestaurantAsync(Restaurant restaurant)
    {
        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();
        return "Restaurant added successfully.";
    }

    public async Task<string> UpdateRestaurantAsync(Restaurant restaurant)
    {
        var existingRestaurant = await _context.Restaurants.FindAsync(restaurant.RestaurantID);
        if (existingRestaurant == null)
        {
            return "Restaurant not found.";
        }

        _context.Entry(existingRestaurant).CurrentValues.SetValues(restaurant);
        await _context.SaveChangesAsync();
        return "Restaurant updated successfully.";
    }

    public async Task<string> DeleteRestaurantAsync(int restaurantId)
    {
        var existingRestaurant = await _context.Restaurants.FindAsync(restaurantId);
        if (existingRestaurant == null)
        {
            return "Restaurant not found.";
        }

        _context.Restaurants.Remove(existingRestaurant);
        await _context.SaveChangesAsync();
        return "Restaurant deleted successfully.";
    }

    public async Task<Restaurant?> GetRestaurantAsync(int restaurantId)
    {
        return await _context.Restaurants.FindAsync(restaurantId);
    }
}