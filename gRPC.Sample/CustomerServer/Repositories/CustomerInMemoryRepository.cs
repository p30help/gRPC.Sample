using CustomerServer.Models;
using CustomerServer.Repositories.Interfaces;

namespace CustomerServer.Repositories
{
    public class CustomerInMemoryRepository : ICustomerRepository
    {
        private readonly List<CustomerModel> _items = new List<CustomerModel>();

        public Task<Guid> AddCustomer(CustomerModel model)
        {
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }

            _items.Add(model);

            return Task.FromResult(model.Id);
        }

        public Task UpdateCustomer(CustomerModel model)
        {
            var res = _items.FirstOrDefault(x => x.Id == model.Id);

            if (res == null)
            {
                throw new Exception("Item not found");
            }

            res.Age = model.Age;
            res.FirstName = model.FirstName;
            res.LastName = model.LastName;

            return Task.CompletedTask;
        }

        public Task<CustomerModel?> GetCustomer(Guid id)
        {
            var res = _items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(res);
        }

        public Task<List<CustomerModel>> GetCustomers()
        {
            return Task.FromResult(_items.ToList());
        }

        public Task DeleteCustomer(Guid id)
        {
            _items.RemoveAll(x => x.Id == id);

            return Task.CompletedTask;
        }
    }

}
