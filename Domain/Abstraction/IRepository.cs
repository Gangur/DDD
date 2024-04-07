namespace Domain.Abstraction
{
    public interface IRepository<TEntity, TEntityId> 
        where TEntity : BaseEntity<TEntityId>
        where TEntityId : class, IEntityId
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity?> FindAsync(TEntityId entityId, CancellationToken cancellationToken);

        Task<ICollection<TEntity>> ListAsync(CancellationToken cancellationToken);

        void Remove(TEntity entity);
    }
}
