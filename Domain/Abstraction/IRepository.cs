namespace Domain.Abstraction
{
    public interface IRepository<TEntity, TEntityId> 
        where TEntity : Entity<TEntityId>
        where TEntityId : class, IEntityId
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity?> FindAsync(TEntityId entityId, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<TEntity>> ListAsync(CancellationToken cancellationToken);

        void Remove(TEntity entity);
    }
}
