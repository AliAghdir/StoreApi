using StoreApi.Contracts;
using StoreApi.Models;

namespace StoreApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private StoreDbContext _context;
        public CustomerRepository(StoreDbContext context)
        {
            _context = context;
        }

        public Task<Customer> Add(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountCustomer()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}