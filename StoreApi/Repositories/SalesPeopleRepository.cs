using StoreApi.Contracts;
using StoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace StoreApi.Repositories
{
    public class SalesPeopleRepository : ISalesPeopleRepository
    {
        private StoreDbContext _context;
        private IMemoryCache _cache;

        public SalesPeopleRepository(StoreDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public IEnumerable<SalesPerson> GetAll()
        {
            return _context.SalesPeople.ToList();
        }

        public async Task<SalesPerson> Add(SalesPerson sales)
        {
            await _context.SalesPeople.AddAsync(sales);
            await _context.SaveChangesAsync();
            return sales;
        }

        public async Task<SalesPerson> Find(int id)
        {
            var cacheSalesPerson = _cache.Get<SalesPerson>(id);
            if (cacheSalesPerson != null)
            {
                return cacheSalesPerson;
            }
            else
            {
                var salesPerson = await _context.SalesPeople.SingleOrDefaultAsync(c => c.SalesPersonId == id);
                var cacheOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(salesPerson.SalesPersonId, salesPerson, cacheOption);
                return salesPerson;
            }
        }

        public async Task<SalesPerson> Update(SalesPerson sales)
        {
            _context.Update(sales);
            await _context.SaveChangesAsync();
            return sales;
        }

        public async Task<SalesPerson> Remove(int id)
        {
            var sales = await _context.SalesPeople.SingleAsync(s => s.SalesPersonId == id);
            _context.SalesPeople.Remove(sales);
            await _context.SaveChangesAsync();
            return sales;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.SalesPeople.AnyAsync(s => s.SalesPersonId == id);
        }
    }
}
