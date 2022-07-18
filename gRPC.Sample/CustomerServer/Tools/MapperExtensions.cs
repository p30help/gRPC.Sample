using CustomerServer.Models;

namespace CustomerServer.Tools
{
    public static class MapperExtensions
    {
        public static CustomerModel ToCustomerModel(this CustomerResponse model)
        {
            return new CustomerModel()
            {
                Age = model.Age,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = Guid.Parse(model.Id),
            };
        }

        public static CustomerResponse ToCustomerModel(this CustomerModel model)
        {
            return new CustomerResponse()
            {
                Age = model.Age,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = model.Id.ToString(),
            };
        }
    }
}
