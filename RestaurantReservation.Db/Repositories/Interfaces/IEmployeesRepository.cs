using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories.Interfaces;

public interface IEmployeesRepository
{
    public Task<Employee> AddEmployeeAsync(Employee employee);
    public Task<bool> UpdateEmployeeAsync(int id, Employee employee);
    public Task<bool> DeleteEmployeeAsync(Employee employee);
    public Task<Employee?> GetEmployeeAsync(int employeeId);
    public Task<IEnumerable<Employee?>> GetEmployeesAsync();
    public Task<IEnumerable<Employee>> GetManagersAsync();
    public Task<double> CalculateAverageOrderAmountAsync(int employeeId);
    public Task SaveChangesAsync();
}