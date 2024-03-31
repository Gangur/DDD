using Domain.Abstraction;

namespace Domain.Customers
{
    public interface ICustomerRepository : IRepository<Customer, CustomerId>
    {
        
    }
}
