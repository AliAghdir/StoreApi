using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using StoreApi.Contracts;
using StoreApi.Models;

namespace StoreApi.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private StoreDbContext _context;
        private IMemoryCache _cache;

        public CustomersRepository(StoreDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Find(int id)
        {
            var cacheCustomer = _cache.Get<Customer>(id);
            if (cacheCustomer != null)
            {
                return cacheCustomer;
            }
            else
            {
                var customer = await _context.Customers.Include(c => c.Orders).SingleOrDefaultAsync(c => c.CustomerId == id);
                var cacheOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(customer.CustomerId, customer, cacheOption);
                return customer;
            }
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