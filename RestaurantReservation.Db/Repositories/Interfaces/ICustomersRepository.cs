using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories.Interfaces;

public interface ICustomersRepository
{
    public Task<Customer?> AddCustomerAsync(Customer customer);
    public Task<bool> UpdateCustomerAsync(int customerId, Customer customer);
    public Task<bool> DeleteCustomerAsync(Customer customer);
    public Task<Customer?> GetCustomerAsync(int customerId);
    public Task<List<Customer>> GetCustomersByPartySizeAsync(int partySize);
    public Task<IEnumerable<Customer?>> GetCustomersAsync();
    public Task SaveChangesAsync();
}