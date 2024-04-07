using Application.Abstraction;
using Domain.Customers;
using Presentation;

namespace Application.Customers.Get
{
    public record GetCustomerQuery(CustomerId CustomerId) : IQuery<CustomerDto>;
}
