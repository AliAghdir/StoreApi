using StoreApi.Models;

namespace StoreApi.Contracts
{
    public interface ISalesPeopleRepository
    {
        IEnumerable<SalesPerson> GetAll();
        Task<SalesPerson> Add(SalesPerson sales);
        Task<SalesPerson> Find(int id);
        Task<SalesPerson> Update(SalesPerson sales);
        Task<SalesPerson> Remove(int id);
        Task<bool> IsExists(int id);
    }
}
