using Domain.Abstraction.Transport;
using System.Linq.Expressions;

namespace Persistence.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> ApplyBaseListParameters<T, TOrderingKey>(this IQueryable<T> query,
                ListParameters parameters,
                Expression<Func<T, TOrderingKey>> orderingKey)
            where T : class
        {
            IOrderedQueryable<T> orderedQuery;
            if (parameters.Descending)
            {
                orderedQuery = query.OrderByDescending(orderingKey);
            }
            else
            {
                orderedQuery = query.OrderBy(orderingKey);
            }

            return orderedQuery
                .Skip(parameters.PageSize * (parameters.PageNumber - 1))
                .Take(parameters.PageSize);
        }
    }
}
