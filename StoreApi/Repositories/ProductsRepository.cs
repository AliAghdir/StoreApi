using StoreApi.Contracts;
using Microsoft.EntityFrameworkCore;
using StoreApi.Models;
using Microsoft.Extensions.Caching.Memory;

namespace StoreApi.Repositories
{
    public class ProductsRepository:IProductsRepository
    {
        private StoreDbContext _context;
        private IMemoryCache _cache;

        public ProductsRepository(StoreDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public async Task<Product> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Find(int id)
        {
            var cacheProduct = _cache.Get<Product>(id);
            if (cacheProduct != null)
            {
                return cacheProduct;
            }
            else
            {
                var product = await _context.Products.SingleOrDefaultAsync(c => c.ProductId == id);
                var cacheOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(product.ProductId, product, cacheOption);
                return product;
            }
        }

        public async Task<Product> Update(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Remove(int id)
        {
            var product = await _context.Products.SingleAsync(c => c.ProductId == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Products.AnyAsync(c => c.ProductId == id);
        }
    }
}
