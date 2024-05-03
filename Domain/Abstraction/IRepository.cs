using Domain.Abstraction.Transport;
using Domain.Products;
using System.Linq.Expressions;

namespace Domain.Abstraction
{
    public interface IRepository<TEntity, TEntityId, TListParameters> 
        where TEntity : BaseEntity<TEntityId>
        where TEntityId : class, IEntityId
        where TListParameters : ListParameters
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity?> FindAsync(TEntityId entityId, CancellationToken cancellationToken);

        Task<TEntity?> TakeAsync(TEntityId entityId, CancellationToken cancellationToken);

        Task<List<TEntity>> ListAsync(TListParameters parameters, CancellationToken cancellationToken);

        Task<int> CountAsync(TListParameters parameters, CancellationToken cancellationToken);

        void Remove(TEntity entity);

        IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, ListParameters parameters);
    }
}
