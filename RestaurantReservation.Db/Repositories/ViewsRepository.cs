using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities.Views;
using Serilog;

namespace RestaurantReservation.Db.Repositories;

public class ViewsRepository
{
    private readonly RestaurantReservationDbContext _context;

    public ViewsRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReservationDetails>> GetReservationsDetailsAsync()
    {
        try
        {
            return await _context.ReservationDetails.ToListAsync();
        }
        catch (Exception e)
        {
            Log.Error(e, "Error getting reservations details");
            throw;
        }
    }

    public async Task<List<EmployeeDetails>> GetEmployeesDetailsAsync()
    {
        try
        {
            return await _context.EmployeeDetails.ToListAsync();
        }
        catch (Exception e)
        {
            Log.Error(e, "Error getting employees details");
            throw;
        }
    }
}