using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities.Views;

namespace RestaurantReservation.Db.Repositories;

public class ViewsRepository
{
    private readonly RestaurantReservationDbContext _context;

    public ViewsRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReservationDetails>> GetReservationsDetails()
    {
        return await _context.ReservationDetails.ToListAsync();
    }

    public async Task<List<EmployeeDetails>> GetEmployeesDetails()
    {
        return await _context.EmployeeDetails.ToListAsync();
    }
}