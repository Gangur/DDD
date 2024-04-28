namespace Domain.Abstraction
{
    public interface IRepository<TEntity, TEntityId> 
        where TEntity : BaseEntity<TEntityId>
        where TEntityId : class, IEntityId
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        ValueTask<TEntity?> FindAsync(TEntityId entityId, CancellationToken cancellationToken);

        Task<TEntity?> TakeAsync(TEntityId entityId, CancellationToken cancellationToken);

        Task<List<TEntity>> ListAsync(CancellationToken cancellationToken);

        void Remove(TEntity entity);
    }
}
