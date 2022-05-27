using StoreApi.Contracts;
using StoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Repositories
{
    public class SalesPeopleRepository : ISalesPeopleRepository
    {
        private StoreDbContext _context;

        public SalesPeopleRepository(StoreDbContext context)
        {
            _context = context;
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
            return await _context.SalesPeople.SingleOrDefaultAsync(s => s.SalesPersonId == id);

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
