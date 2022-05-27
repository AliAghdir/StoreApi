using StoreApi.Models;

namespace StoreApi.Contracts
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAll();
        Task<Product> Add(Product product);
        Task<Product> Find(int id);
        Task<Product> Update(Product product);
        Task<Product> Remove(int id);
        Task<bool> IsExists(int id);
    }
}
