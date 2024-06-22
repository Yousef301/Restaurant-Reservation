using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;
using Serilog;

namespace RestaurantReservation.Db.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public CustomersRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> AddCustomerAsync(Customer customer)
        {
            try
            {
                if (await EmailExistsAsync(customer.Email))
                {
                    return null;
                }

                await _context.Customers.AddAsync(customer);
                await SaveChangesAsync();
                return customer;
            }
            catch (Exception e)
            {
                Log.Error(e, "Error adding customer");
                throw;
            }
        }

        public async Task<bool> UpdateCustomerAsync(int customerId, Customer customer)
        {
            try
            {
                var existingCustomer = await _context.Customers.FindAsync(customerId);
                if (existingCustomer == null)
                {
                    return false;
                }

                _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
                await SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e, "Error updating customer");
                throw;
            }
        }

        public async Task<bool> DeleteCustomerAsync(Customer customer)
        {
            try
            {
                var existingCustomer = await _context.Customers.FindAsync(customer.CustomerID);
                if (existingCustomer == null)
                {
                    return false;
                }

                _context.Customers.Remove(existingCustomer);
                await SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e, "Error deleting customer");
                throw;
            }
        }

        public async Task<IEnumerable<Customer?>> GetCustomersAsync()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, "Error getting customers");
                throw;
            }
        }

        public async Task<Customer?> GetCustomerAsync(int customerId)
        {
            try
            {
                return await _context.Customers.FindAsync(customerId);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error getting customer");
                throw;
            }
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            try
            {
                return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error getting customer by email");
                throw;
            }
        }

        private async Task<bool> EmailExistsAsync(string email)
        {
            try
            {
                return await _context.Customers.AnyAsync(c => c.Email == email);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error checking if email exists");
                throw;
            }
        }

        public async Task<List<Customer>> GetCustomersByPartySizeAsync(int partySize)
        {
            try
            {
                return await _context.Customers.FromSqlInterpolated($"EXEC GetCustomersByPartySize {partySize}")
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, "Error getting customers by party size");
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
                Log.Error(e, "Error saving changes");
                throw;
            }
        }
    }
}