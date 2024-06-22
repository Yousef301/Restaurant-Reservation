using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Enums;
using RestaurantReservation.Db.Repositories.Interfaces;
using Serilog;

namespace RestaurantReservation.Db.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly RestaurantReservationDbContext _context;

    public EmployeesRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<Employee> AddEmployeeAsync(Employee employee)
    {
        try
        {
            await _context.Employees.AddAsync(employee);
            await SaveChangesAsync();
            return employee;
        }
        catch (Exception e)
        {
            Log.Error(e, "Error occurred while adding employee");
            throw;
        }
    }

    public async Task<bool> UpdateEmployeeAsync(int id, Employee employee)
    {
        try
        {
            var existingEmployee = await _context.Employees.FindAsync(employee.EmployeeID);
            if (existingEmployee == null)
            {
                return false;
            }

            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "Error occurred while updating employee");
            throw;
        }
    }

    public async Task<bool> DeleteEmployeeAsync(Employee employee)
    {
        try
        {
            var existingEmployee = await _context.Employees.FindAsync(employee.EmployeeID);
            if (existingEmployee == null)
            {
                return false;
            }

            _context.Employees.Remove(existingEmployee);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "Error occurred while deleting employee");
            throw;
        }
    }

    public async Task<Employee?> GetEmployeeAsync(int employeeId)
    {
        try
        {
            return await _context.Employees.FindAsync(employeeId);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error occurred while getting employee");
            throw;
        }
    }

    public async Task<List<Employee>> ListManagerAsync()
    {
        try
        {
            return await _context.Employees.Where(e => e.Position == Position.Manager).ToListAsync();
        }
        catch (Exception e)
        {
            Log.Error(e, "Error occurred while listing managers");
            throw;
        }
    }

    public async Task<IEnumerable<Employee?>> GetEmployeesAsync()
    {
        try
        {
            return await _context.Employees.ToListAsync();
        }
        catch (Exception e)
        {
            Log.Error(e, "Error occurred while getting employees");
            throw;
        }
    }

    public async Task<double> CalculateAverageOrderAmountAsync(int employeeId)
    {
        try
        {
            var employeeOrdersCount = await _context.Orders.CountAsync(o => o.EmployeeID == employeeId);
            var totalOrdersCount = await _context.Orders.CountAsync();

            return (double)employeeOrdersCount / totalOrdersCount;
        }
        catch (Exception e)
        {
            Log.Error(e, "Error occurred while calculating average order amount");
            throw;
        }
    }

    public async Task<IEnumerable<Employee>> GetManagersAsync()
    {
        try
        {
            return await _context.Employees
                .Where(e => e.Position == Position.Manager)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Log.Error(e, "Error occurred while getting managers");
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
            Log.Error(e, "Error occurred while saving changes");
            throw;
        }
    }
}