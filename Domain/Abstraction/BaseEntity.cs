namespace Domain.Abstraction
{
    public abstract class BaseEntity<TEntityId> : AggregateRoot
        where TEntityId : class, IEntityId
    {
        public TEntityId Id { get; internal set; }
    }
}
