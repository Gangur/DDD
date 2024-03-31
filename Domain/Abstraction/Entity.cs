namespace Domain.Abstraction
{
    public abstract class Entity<TEntityId>
        where TEntityId : class, IEntityId
    {
        public TEntityId Id { get; }

        private readonly List<IDomainEvent<IEntityId>> _domainEvents = new();

        protected void Raise(IDomainEvent<IEntityId> domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IReadOnlyCollection<IDomainEvent<IEntityId>> DomainEvents { get => _domainEvents; }
    }
}
