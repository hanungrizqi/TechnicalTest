using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace WebAPI.Cls
{
    public class ClsCRUD
    {
        private readonly _dbContext _context;

        public ClsCRUD(_dbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomerAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(int id, Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);
            if (existingCustomer == null)
            {
                return false;
            }

            existingCustomer.CustomerCode = customer.CustomerCode;
            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.CustomerAddress = customer.CustomerAddress;
            existingCustomer.ModifiedBy = customer.ModifiedBy;
            existingCustomer.ModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
