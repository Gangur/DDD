using Application.Abstraction;
using Domain.Customers;

namespace Application.Customers.Get
{
    public record GetCustomerQuery(CustomerId CustomerId) : IQuery<Customer>;
}
