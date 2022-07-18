using CustomerServer.Models;

namespace CustomerServer.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<Guid> AddCustomer(CustomerModel model);

        public Task UpdateCustomer(CustomerModel model);

        public Task<CustomerModel?> GetCustomer(Guid id);

        public Task<List<CustomerModel>> GetCustomers();

        public Task DeleteCustomer(Guid id);
    }
}
