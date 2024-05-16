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

        public async Task<string> AddCustomer(Customer customer)
        {
            if (await EmailExists(customer.Email))
            {
                return "Customer with this email already exists.";
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return "Customer added successfully.";
        }

        public async Task<string> UpdateCustomer(Customer customer)
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

        public async Task<string> DeleteCustomer(int customerId)
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

        public async Task<Customer?> GetCustomer(int customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        private async Task<bool> EmailExists(string email)
        {
            return await _context.Customers.AnyAsync(c => c.Email == email);
        }
    }
}