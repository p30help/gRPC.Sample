// See https://aka.ms/new-console-template for more information

using CustomerServer;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");

var channel = GrpcChannel.ForAddress("https://localhost:7185");
var client = new Customers.CustomersClient(channel);

// add new customer

Console.WriteLine($"Adding customers ...");

var newCustomerRes = client.AddCustomer(new AddCustomerRequest()
{
    Age = 25,
    FirstName = "Mahdi",
    LastName = "Radiom"
});

Console.WriteLine($"Customer 1 added");

client.AddCustomer(new AddCustomerRequest()
{
    Age = 50,
    FirstName = "Joseph",
    LastName = "Anderson"
});

Console.WriteLine($"Customer 2 added");

client.AddCustomer(new AddCustomerRequest()
{
    Age = 22,
    FirstName = "Kasra",
    LastName = "Ahooraee"
});

Console.WriteLine($"Customer 3 added");
Console.WriteLine($"------------------------");

Console.WriteLine($"Getting all customers");

// get customers
using (var call = client.GetCustomers(new GetCustomersRequest()))
{
    while (await call.ResponseStream.MoveNext(CancellationToken.None))
    {
        var item = call.ResponseStream.Current;
        Console.WriteLine($"{item.FirstName} {item.LastName} - Age : {item.Age}");
    }
}

Console.WriteLine($"-------------------");

Console.WriteLine($"Updating customer");

// update
client.UpdateCustomer(new UpdateCustomerRequest()
{
    Id = newCustomerRes.Id.ToString(),
    Age = 33,
    FirstName = "Mahdi",
    LastName = "Radi"
});

Console.WriteLine($"One customer updated");

Console.WriteLine($"-------------------");

Console.WriteLine($"Get updated customer");

var customerRadi = client.GetCustomer(new GetCustomerRequest()
{
    Id = newCustomerRes.Id.ToString()
});

Console.WriteLine($"{customerRadi.FirstName} {customerRadi.LastName} - Age : {customerRadi.Age}");

Console.WriteLine($"-------------------");

Console.WriteLine($"Deleting customer");

client.DeleteCustomer(new DeleteCustomerRequest()
{
    Id = newCustomerRes.Id.ToString()
});

Console.WriteLine($"One customer Deleted");
Console.WriteLine($"-------------------");


// get customers

Console.WriteLine($"Getting all customers");

using (var call = client.GetCustomers(new GetCustomersRequest()))
{
    while (await call.ResponseStream.MoveNext(CancellationToken.None))
    {
        var item = call.ResponseStream.Current;
        Console.WriteLine($"{item.FirstName} {item.LastName} - Age : {item.Age}");
    }
}

Console.WriteLine($"-------------------");

Console.ReadLine();