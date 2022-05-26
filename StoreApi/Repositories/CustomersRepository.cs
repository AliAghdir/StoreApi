using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using StoreApi.Contracts;
using StoreApi.Models;

namespace StoreApi.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private StoreDbContext _context;
        public CustomersRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Find(int id)
        {
            return await _context.Customers
            .Include(c => c.Orders)
            .SingleOrDefaultAsync(c => c.CustomerId == id);
        }

        public IEnumerable<Customer> GetAll()=>
              _context.Customers.ToList();

        public async Task<int> GetCountCustomer()
        {
            return await _context.Customers.CountAsync();
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Customers.AnyAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> Remove(int id)
        {
            var customer = await _context.Customers.SingleAsync(c => c.CustomerId == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}