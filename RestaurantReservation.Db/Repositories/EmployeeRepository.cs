using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Enums;

namespace RestaurantReservation.Db.Repositories;

public class EmployeeRepository
{
    private readonly RestaurantReservationDbContext _context;

    public EmployeeRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return "Employee added successfully.";
    }

    public async Task<string> UpdateEmployee(Employee employee)
    {
        var existingEmployee = await _context.Employees.FindAsync(employee.EmployeeID);
        if (existingEmployee == null)
        {
            return "Employee not found.";
        }

        _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
        await _context.SaveChangesAsync();
        return "Employee updated successfully.";
    }

    public async Task<string> DeleteEmployee(int employeeId)
    {
        var existingEmployee = await _context.Employees.FindAsync(employeeId);
        if (existingEmployee == null)
        {
            return "Employee not found.";
        }

        _context.Employees.Remove(existingEmployee);
        await _context.SaveChangesAsync();
        return "Employee deleted successfully.";
    }

    public async Task<Employee?> GetEmployee(int employeeId)
    {
        return await _context.Employees.FindAsync(employeeId);
    }

    public async Task<List<Employee>> ListManager()
    {
        return await _context.Employees.Where(e => e.Position == Position.Manager).ToListAsync();
    }

    public async Task<double> CalculateAverageOrderAmount(int employeeId)
    {
        var employeeOrdersCount = await _context.Orders.CountAsync(o => o.EmployeeID == employeeId);
        var totalOrdersCount = await _context.Orders.CountAsync();

        return (double)employeeOrdersCount / totalOrdersCount;
    }
}