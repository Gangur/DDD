using Domain.Abstraction;
using Domain.Abstraction.Transport;

namespace Domain.LineItems
{
    public interface ILineItemRepository : IRepository<LineItem, LineItemId, ListParameters>
    {

    }
}
