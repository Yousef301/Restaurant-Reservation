using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories
{
    public class CustomerRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public CustomerRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddCustomerAsync(Customer customer)
        {
            if (await EmailExistsAsync(customer.Email))
            {
                return "Customer with this email already exists.";
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return "Customer added successfully.";
        }

        public async Task<string> UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.CustomerID);
            if (existingCustomer == null)
            {
                return "Customer not found.";
            }

            _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
            await _context.SaveChangesAsync();
            return "Customer updated successfully.";
        }

        public async Task<string> DeleteCustomerAsync(int customerId)
        {
            var existingCustomer = await _context.Customers.FindAsync(customerId);
            if (existingCustomer == null)
            {
                return "Customer not found.";
            }

            _context.Customers.Remove(existingCustomer);
            await _context.SaveChangesAsync();
            return "Customer deleted successfully.";
        }

        public async Task<Customer?> GetCustomerAsync(int customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        private async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Customers.AnyAsync(c => c.Email == email);
        }

        public async Task<List<Customer>> GetCustomersByPartySizeAsync(int partySize)
        {
            return _context.Customers.FromSqlInterpolated($"GetCustomersByPartySize {partySize}").ToList();
        }
    }
}