using Domain.Abstraction;
using Domain.Abstraction.Transport;

namespace Domain.Customers
{
    public interface ICustomerRepository : IRepository<Customer, CustomerId, ListParameters>
    {
        
    }
}
