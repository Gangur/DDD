using Application.Abstraction;
using Domain.Abstraction.Transport;
using Presentation;

namespace Application.Customers.List
{
    public record ListCustomersQuery(ListParameters ListParameters) : IQuery<ListResultDto<CustomerDto>>;
}
