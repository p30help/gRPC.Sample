using CustomerServer.Models;
using CustomerServer.Repositories.Interfaces;
using CustomerServer.Tools;
using Grpc.Core;

namespace CustomerServer.Services
{
    public class CustomersService : Customers.CustomersBase
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomersService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public override async Task<AddCustomerResponse> AddCustomer(AddCustomerRequest request, ServerCallContext context)
        {
            var res = await _customerRepo.AddCustomer(new CustomerModel()
            {
                Age = request.Age,
                FirstName = request.FirstName,
                LastName = request.LastName
            });

            return new AddCustomerResponse()
            {
                Id = res.ToString()
            };
        }

        public override async Task GetCustomers(GetCustomersRequest request, IServerStreamWriter<CustomerResponse> responseStream, ServerCallContext context)
        {
            var items = await _customerRepo.GetCustomers();

            foreach (var item in items)
            {
                await Task.Delay(100);
                await responseStream.WriteAsync(item.ToCustomerModel());
            }
        }


        public override async Task<CustomerResponse> GetCustomer(GetCustomerRequest request, ServerCallContext context)
        {
            var item = await _customerRepo.GetCustomer(Guid.Parse(request.Id));

            if (item == null)
            {
                throw new Exception("Item not found");
            }

            return item.ToCustomerModel();
        }

        public override async Task<DeleteCustomerResponse> DeleteCustomer(DeleteCustomerRequest request, ServerCallContext context)
        {
            await _customerRepo.DeleteCustomer(Guid.Parse(request.Id));

            return new DeleteCustomerResponse();
        }

        public override async Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest request, ServerCallContext context)
        {
            await _customerRepo.UpdateCustomer(new CustomerModel()
            {
                Age = request.Age,
                FirstName = request.FirstName,
                Id = Guid.Parse(request.Id),
                LastName = request.LastName
            });

            return new UpdateCustomerResponse();
        }
    }
}
